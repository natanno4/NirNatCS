using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Configuration;
using System.Threading;

namespace ImageGui.Communication
{
    class GuiClient : IClient
    {
        public event EventHandler<string> MessageRecived;
        private TcpClient TClient;
        private int portNumber;
        private static Mutex rMutex;
        private static Mutex wMutex;
        private IPEndPoint ipEndPoint;
        private static GuiClient instance;
        
        public static GuiClient instanceS 
        {
            get {

                if (instance == null)
                {
                    instance = new GuiClient();
                }
                return instance;
            }
        }


        private GuiClient()
        {
            this.ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.0"), 7000);
            this.Connect(this.ipEndPoint);

        }
        private void Connect(IPEndPoint ep)
        {
            //may be will be changed
            try 
            {
                TClient = new TcpClient();
                TClient.Connect(this.ipEndPoint);
                Console.WriteLine("connect sucessfully");
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        void Disconnect()
        {
            TClient.Close();
            Console.WriteLine("disconnect successfully");
        }
        void Write(string cmd)
        {
            new Task(() =>
            {
                using (NetworkStream stream = TClient.GetStream())
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    wMutex.WaitOne();
                    writer.Write(cmd);
                    wMutex.ReleaseMutex();
                }
            });
        }
        public void Read()
        {
            string buffer;
            using (NetworkStream stream = TClient.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            {
                rMutex.WaitOne();
                buffer = reader.ReadString();
                rMutex.ReleaseMutex();
                if (buffer != null)
                { 
                    MessageRecived?.Invoke(this, buffer);
                }
            }
        }

        void IClient.Connect(IPEndPoint endP)
        {
            throw new NotImplementedException();
        }

        void IClient.Write(string command)
        {
            throw new NotImplementedException();
        }

        void IClient.Disconnect()
        {
            throw new NotImplementedException();
        }
    }
}
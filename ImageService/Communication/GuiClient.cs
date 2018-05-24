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
using ImageService.Logging.Modal;
using Communication.Event;
using ImageService.Commands;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Communication
{
    public class GuiClient : IClient
    {
        public event EventHandler<MsgCommand> CommandRecived;
        private System.Net.Sockets.TcpClient TClient;
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
            this.Connect();

        }
        public void Connect()
        {
            //may be will be changed
            try 
            {
                TClient = new System.Net.Sockets.TcpClient();
                TClient.Connect(this.ipEndPoint);
                Console.WriteLine("connect sucessfully");
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void Disconnect()
        {
            TClient.Close();
            Console.WriteLine("disconnect successfully");
        }


        public void Write(CommandRecievedEventArgs e)
        {
            new Task(() =>
            {
                using (NetworkStream stream = TClient.GetStream())
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    wMutex.WaitOne();
                    string send = JsonConvert.SerializeObject(e);
                    writer.Write(send);
                    wMutex.ReleaseMutex();
                }
            }).Start();
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
                   MsgCommand msg = MsgCommand.FromJSON(buffer);
                   CommandRecived?.Invoke(this, msg);
                }
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ImageGui.Communication
{
    class GuiClient : IClient
    {
        private TcpClient TClient;
        
        public GuiClient(TcpClient t)
        {
            this.TClient = t;
        }
        void Connect(IPEndPoint ep)
        {
            TClient.Connect(ep);
            Console.WriteLine("connect sucessfully");
        }
        void Disconnect()
        {
            TClient.Close();
            Console.WriteLine("disconnect successfully");
        }
        void Write(string cmd)
        {
            using (NetworkStream stream = TClient.GetStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(cmd);
            }
        }
        String Read()
        {
            char[] buffer = new char[120];
            int numberOfBytesRead = 0;
            using (NetworkStream stream = TClient.GetStream())
            using (BinaryReader reader = new BinaryReader(stream))
            {
                while (reader.Read(buffer, 0, 120) != 120)
                {
                    // need to complet
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using ImageService.Commands;
using Newtonsoft.Json;
using ImageService.Infrastructure.Enums;
using ImageService.Controller;
using System.Threading;
using ImageService.Logging;

namespace Communication
{
    public class ClientHandler : IClientHandler
    {
        private List<TcpClient> clientList;
        private ImageController controller;
        public static Mutex rMutex;
        public static Mutex wMutex;
        private ILoggingService logging;

        public ClientHandler(List<TcpClient> c, ImageController ic, ILoggingService logs)
        {
            this.clientList = c;
            this.controller = ic;
            rMutex = new Mutex();
            wMutex = new Mutex();
            this.logging = logs;
           
        }

        public void HandleClient(TcpClient client)
        {

            new Task(() =>
            {
                while (true)
                {
                    NetworkStream stream = client.GetStream();
                    BinaryWriter writer = new BinaryWriter(stream);
                    BinaryReader reader = new BinaryReader(stream);
                    rMutex.WaitOne();
                    string recived = reader.ReadString();
                    rMutex.ReleaseMutex();
                    MsgCommand msg = JsonConvert.DeserializeObject<MsgCommand>(recived);
                    //להוסיף command.enum.closeCleintHandler
                    if ((int)msg.commandID == (int)CommandEnum.CloseCommand)
                    {
                        clientList.Remove(client);
                        client.Close();
                        break;
                    }
                    else
                    {
                        bool res;
                        string result = this.controller.ExecuteCommand((int)msg.commandID, msg.args, out res);
                        wMutex.WaitOne();
                        writer.Write(result);
                        wMutex.ReleaseMutex();
                    }
                      
                }
            }).Start();
        }
    }
}

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
using ImageService.Logging.Modal;
using Infrastructure;


namespace ImageService.ServiceCommunication
{
    public class ClientHandler : IClientHandler
    {
        private List<TcpClient> clientList;
        private IImageController controller;
        public static Mutex rMutex;
        public static Mutex wMutex;
        private ILoggingService logging;

        public ClientHandler(List<TcpClient> c, IImageController ic, ILoggingService logs)
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
                while (client.Connected)
                {
                    try
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
                            this.logging.Log("close client", MessageTypeEnum.INFO);
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
                    } catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        this.logging.Log("faild handle client", MessageTypeEnum.FAIL);
                        client.Close();
                        this.logging.Log("close client", MessageTypeEnum.INFO);
                    }
                      
                }
            }).Start();
        }


        public void notifyClient(TcpClient client, MsgCommand msg)
        {
            NetworkStream stream = client.GetStream();
            BinaryWriter writer = new BinaryWriter(stream);
            string writeCommand = msg.ToJSON();
            wMutex.WaitOne();
            writer.Write(writeCommand);
            wMutex.ReleaseMutex();

        }
    }
}

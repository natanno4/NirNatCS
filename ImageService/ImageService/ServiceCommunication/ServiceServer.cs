using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using ImageService.Controller;
using ImageService.Logging;
using ImageService.Logging.Modal;
using Infrastructure;

namespace ImageService.ServiceCommunication
{
    public class ServiceServer : IServiceServer
    {
        private int port;
        private TcpListener listener;
        private IClientHandler handler;
        private List<TcpClient> clients;
        private ILoggingService logging;

        public ServiceServer(int prt, IImageController controller, ILoggingService logService)
        {
            this.logging = logService;
            this.port = prt;
            this.handler = new ClientHandler(clients, controller, logService);
            clients = new List<TcpClient>();
        }
        public void Start()
        {
            try
            {
                this.logging.LogAdded += this.onNotifyClients;
                IPEndPoint pnt = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
                listener = new TcpListener(pnt);
                listener.Start();
                // print of connections
                Task tsk = new Task(() =>
                {
                    while (true)
                    {
                        try
                        {
                            TcpClient client = listener.AcceptTcpClient();
                            // success in recieve
                            clients.Add(client);
                            handler.HandleClient(client);
                        }
                        catch (Exception e)
                        {  
                            Console.WriteLine(e.ToString());
                            this.logging.Log("error in tcp server", MessageTypeEnum.FAIL);
                            break;
                        }

                    } 
                });
                tsk.Start();
            } catch(Exception t)
            {
                Console.WriteLine(t.ToString());
            }
        }
        public void Stop()
        {
            listener.Stop();
        }


        public void onNotifyClients(object sender, MsgCommand msg)
        {
            new Task(() =>
            {
                foreach (TcpClient C in clients)
                {
                    try
                    {
                        this.handler.notifyClient(C, msg);
                    } catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        this.logging.Log("error in tcp server", MessageTypeEnum.FAIL);
                        C.Close();
                        this.clients.Remove(C);
                        this.logging.Log("close client", MessageTypeEnum.INFO);
                    }
                }
            }).Start();

        }

    }
}

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
using Communication;

namespace ImageService.ServiceCommunication
{
    public class ServiceServer : IServiceServer
    {
        private int port;
        private string IP;
        private TcpListener listener;
        private IClientHandler handler;
        private List<TcpClient> clients;
        private ILoggingService logging;

        public ServiceServer(IImageController controller, ILoggingService logService)
        {
            CommunicationInfo com = new CommunicationInfo(); 
            this.logging = logService;
            this.port = com.port;
            this.IP = com.IPNumber;
            this.handler = new ClientHandler(clients, controller, logService);
            clients = new List<TcpClient>();
        }
        public void Start()
        {
            try
            {
                this.logging.LogAdded += this.onNotifyClients;
                IPEndPoint pnt = new IPEndPoint(IPAddress.Parse(this.IP), this.port);
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
                        
                        C.Close();
                        this.clients.Remove(C);
                        
                    }
                }
            }).Start();

        }

    }
}

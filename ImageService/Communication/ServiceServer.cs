using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using ImageService.Controller;
using ImageService.Logging;

namespace Communication
{
    public class ServiceServer : IServiceServer
    {
        private int port;
        private TcpListener listener;
        private IClientHandler handler;
        private List<TcpClient> clients;
        private ILoggingService logging;

        public ServiceServer(int prt, ImageController controller, ILoggingService logService)
        {
            this.logging = logService;
            this.port = prt;
            this.handler = new ClientHandler(clients, controller, logService);
            clients = new List<TcpClient>();
        }
        public void Start()
        {
            IPEndPoint pnt = new IPEndPoint(IPAddress.Parse("127.0.0.0"), port);
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
                    }

                }
            });
            tsk.Start();
        }
        public void Stop()
        {
            listener.Stop();
        }

    }
}

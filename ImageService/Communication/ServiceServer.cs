using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Communication
{
    public class ServiceServer : IServiceServer
    {
        private int port;
        private TcpListener listener;
        private IHandler handler;

        public ServiceServer(int prt, IHandler hand)
        {
            this.port = prt;
            this.handler = hand;
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
                        handler.handle(client);
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

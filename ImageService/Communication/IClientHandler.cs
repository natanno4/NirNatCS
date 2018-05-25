using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using ImageService.Commands;

namespace Communication
{
    public interface IClientHandler
    {
        void HandleClient(TcpClient client);
        void notifyClient(TcpClient client, MsgCommand msg);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using ImageService.Logging.Modal;

namespace Communication
{
    public interface IClient
    {
        event EventHandler<MessageRecievedEventArgs> MessageRecived;
        void Connect(IPEndPoint endP);
        void Read();
        void Write(String command);
        void Disconnect();
    }
}

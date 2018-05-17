using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Communication
{
    interface IClient
    {
        void Connect(IPEndPoint endP);
        void Read();
        void Write(String command);
        void Disconnect();
    }
}

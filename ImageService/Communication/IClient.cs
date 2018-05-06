using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ImageGui.Communication
{
    interface IClient
    {
        void Connect(IPEndPoint endP);
        String Read();
        void Write(String command);
        void Disconnect();
    }
}

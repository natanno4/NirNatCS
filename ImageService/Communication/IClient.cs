using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using ImageService.Logging.Modal;
using Communication.Event;
using ImageService.Commands;

namespace Communication
{
    public interface IClient
    {
        event EventHandler<MsgCommand> CommandRecived;
        void Connect();
        void Disconnect();
        void Write(CommandRecievedEventArgs e);
        void Read();
    }
}


using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Logging
{
    public class LoggingService : ILoggingService
    {
        public ObservableCollection<string> listOfLogs { get; set; }
        
        public LoggingService()
        {
            this.listOfLogs = new ObservableCollection<string>();
        } 
        public event EventHandler<MessageRecievedEventArgs> MessageRecieved;
        public void Log(string message, MessageTypeEnum type)
        {
            MessageRecievedEventArgs args = new MessageRecievedEventArgs();
            this.listOfLogs.Add(message + ";" + type.ToString());
            args.Message = message;
            args.Status = type;
            MessageRecieved?.Invoke(this, args);
        }
    }
}

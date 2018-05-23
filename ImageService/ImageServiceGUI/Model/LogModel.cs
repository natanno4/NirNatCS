using System.Collections.ObjectModel;
using System.ComponentModel;
using Communication;
using ImageService.Logging.Modal;
using ImageService.Commands;
using ImageService.Infrastructure.Enums;
using Communication.Event;


namespace ImageServiceGUI.Model
{
    class LogModel : ILogModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private TcpClient client;
        private ObservableCollection<MessageRecievedEventArgs> m_Logs;

        public LogModel()
        {
            this.client = GuiClient.instanceS;
            this.client.CommandRecived += this.OnCommandRecieved;
            string[] args = new string[5];
            CommandRecievedEventArgs cmd = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, args, null);
            client.Write(cmd);
;        }


        public ObservableCollection<MessageRecievedEventArgs> Logs
        {
            get
            {
                return Logs;
            }

            set
            {
                Logs = value;
                NotifyPropertyChanged("Logs");
            }
        }



        public void NotifyPropertyChanged(string propname)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
            }
        }

        public void OnCommandRecieved(object sender, MsgCommand m)
        {
            if ((int)m.commandID == (int)(CommandEnum.LogCommand))
            {

            }
        }


    }
}

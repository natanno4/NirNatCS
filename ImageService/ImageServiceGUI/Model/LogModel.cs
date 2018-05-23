using System.Collections.ObjectModel;
using System.ComponentModel;
using Communication;
using ImageService.Logging.Modal;
using ImageService.Commands;
using ImageService.Infrastructure.Enums;

namespace ImageServiceGUI.Model
{
    class LogModel : ILogModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private IClient client;
        private ObservableCollection<MessageRecievedEventArgs> m_Logs;

        public LogModel()
        {
            this.client = GuiClient.instanceS;
            this.client.MessageRecived += this.OnMessageRecieved;
           
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

        public void OnMessageRecieved(object sender, MessageRecievedEventArgs m)
        {
            if ((int)MsgCommand.FromJSON(m.Message).commandID == (int)(CommandEnum.LogCommand))
            {

            }
        }


    }
}

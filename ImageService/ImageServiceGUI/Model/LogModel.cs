using System.Collections.ObjectModel;
using System.ComponentModel;
using Communication;
using ImageService.Logging.Modal;
using Infrastructure;
using ImageService.Infrastructure.Enums;
using Newtonsoft.Json;
using System;

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
            this.client.CommandRecived += this.OnCommandRecieved;
            string[] args = new string[5];
            MsgCommand cmd = new MsgCommand((int)CommandEnum.LogCommand, args);
            client.Write(cmd);
            this.m_Logs = new ObservableCollection<MessageRecievedEventArgs>();
        }


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
                ObservableCollection<string> collection = JsonConvert.
                    DeserializeObject<ObservableCollection<string>>(m.args[0]);
                foreach (string log in collection)
                {
                    string[] logInfo = log.Split(';');
                    int type = Int32.Parse(logInfo[1]);
                    this.m_Logs.Add(new MessageRecievedEventArgs((MessageTypeEnum)type, logInfo[2]));
                }
            } else
            {
                if((int)m.commandID == (int)CommandEnum.AddLogCommand)
                {
                    this.addNewLog(m);
                }
            }

       
        }

        private void addNewLog(MsgCommand msg)
        {
            MessageRecievedEventArgs m = MessageRecievedEventArgs.FromJSON(msg.args[0]);
            this.m_Logs.Add(m);
        }


    }
}

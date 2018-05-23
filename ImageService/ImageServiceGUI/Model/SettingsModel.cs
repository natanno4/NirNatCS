using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Communication;
using Communication.Event;
using ImageService.Infrastructure.Enums;
using ImageService.Commands;

namespace Model {
    public class SettingsModel : ISettingsModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string saveLogName;
        private string OutPutDir;
        private string saveSourceName;
        private TcpClient client;
        private string tumbNailSize;
        private ObservableCollection<string> handlersModel;
        public void NotifyPropertyChanged(string propname)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
            }
        }

        public SettingsModel()
        {
            handlers = new ObservableCollection<string>();
            this.client = GuiClient.instanceS;
            string[] args = new string[5];
            client.CommandRecived += OnCommandRecived;
            CommandRecievedEventArgs cmd = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, args, null);
            client.Write(cmd);
        }


        public void OnCommandRecived(object sender, MsgCommand msg) {
            if ((int)msg.commandID == (int) CommandEnum.GetConfigCommand)
            {
                this.OutPutDir = msg.args[0];
                this.saveSourceName = msg.args[1];
                this.saveLogName = msg.args[2];
                this.tumbNailSize = msg.args[3];
                string[] h = msg.args[4].Split(';');
                foreach (string handler in h)
                {
                    handlersModel.Add(handler);
                }
            }
        }

        public string logName
        {
            get
            {
                return this.saveLogName;
            }

            set
            {
                saveLogName = value;
                NotifyPropertyChanged("logName");
            }
        }

        public string outPut
        {
            get
            {
                return this.OutPutDir;
            }

            set
            {
                OutPutDir = value;
                NotifyPropertyChanged("outPut");
            }
        }

        public string sourceNmae
        {
            get
            {
                return this.saveSourceName;
            }

            set
            {
                this.saveSourceName = value;
                NotifyPropertyChanged("sourceNmae");
            }
        }

        public string thumbNail
        {
            get
            {
                return this.tumbNailSize;
            }

            set
            {
                this.tumbNailSize = value;
                NotifyPropertyChanged("thumbNail");
            }
        }

        public ObservableCollection<string> handlers
        {
            get
            {
                return this.handlersModel;
            }
            set
            {
                this.handlersModel = value;
                NotifyPropertyChanged("handlers");
            }
        }
   
    }
}

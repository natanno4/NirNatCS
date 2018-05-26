using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Communication;
using Communication.Event;
using ImageService.Infrastructure.Enums;
using Infrastructure;


namespace Model {
    public class SettingsModel : ISettingsModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string m_LogName;
        private string m_OutPutDir;
        private string m_SourceName;
        private string m_tumbNailSize;
        private IClient client;
        private string m_SelectedHandler;
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
            MsgCommand cmd = new MsgCommand((int)CommandEnum.GetConfigCommand, args);
            this.sendCommand(cmd);
            this.defaultConfig();
        }


        public void OnCommandRecived(object sender, MsgCommand msg) {
            if ((int)msg.commandID == (int)CommandEnum.GetConfigCommand)
            {
                OutPutDir = msg.args[0];
                SourceName = msg.args[1];
                LogName = msg.args[2];
                TumbNail = msg.args[3];
                string[] h = msg.args[4].Split(';');
                foreach (string handler in h)
                {
                    handlersModel.Add(handler);
                }
            }
            else
            {
                if((int)msg.commandID == (int)CommandEnum.RemoveHandlerCommand) {
                    this.removeHandler(msg);
                }
            }    
        }

        public string LogName
        {
            get
            {
                return this.m_LogName;
            }

            set
            {
                this.m_LogName = value;
                NotifyPropertyChanged("LogName");
            }
        }

        public string OutPutDir
        {
            get
            {
                return this.m_OutPutDir;
            }

            set
            {
                this.m_OutPutDir = value;
                NotifyPropertyChanged("OutPutDir");
            }
        }

        public string SourceName
        {
            get
            {
                return this.m_SourceName;
            }

            set
            {
                this.m_SourceName = value;
                NotifyPropertyChanged("SourceName");
            }
        }

        public string TumbNail
        {
            get
            {
                return this.m_tumbNailSize;
            }

            set
            {
                this.m_tumbNailSize = value;
                NotifyPropertyChanged("TumbNail");
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

        public string SelectedHandler
        {
            get
            {
                return this.m_SelectedHandler;
            }
            set
            {
                this.m_SelectedHandler = value;
                NotifyPropertyChanged("SelectedHandler");
            }

        }
       

        public void sendCommand(MsgCommand msg)
        {
            this.client.Write(msg);
        }


        public void removeHandler(MsgCommand msg)
        {
            string handler = msg.args[0];
            this.handlers.Remove(handler);
        }


        private void defaultConfig()
        {
            this.OutPutDir = "NO connection";
            this.SourceName = "NO connection";
            this.LogName = "NO connection";
            this.TumbNail = "NO connection";
           
    }

    }
}

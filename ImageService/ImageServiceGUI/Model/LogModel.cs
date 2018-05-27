using System.Collections.ObjectModel;
using System.ComponentModel;
using Communication;
using ImageService.Logging.Modal;
using Infrastructure;
using ImageService.Infrastructure.Enums;
using Newtonsoft.Json;
using System;
using System.Windows;

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
            this.m_Logs = new ObservableCollection<MessageRecievedEventArgs>();
            this.client.CommandRecived += this.OnCommandRecieved;
            string[] args = new string[5];
            MsgCommand cmd = new MsgCommand((int)CommandEnum.LogCommand, args);
            client.SendAndRecived(cmd);
            
        }


        public ObservableCollection<MessageRecievedEventArgs> Logs
        {
            get
            {
                return this.m_Logs;
            }

            set
            {
                this.m_Logs = value;
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
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    foreach (string log in collection)
                    {
                        string[] logInfo = log.Split(';');
                        this.m_Logs.Add(new MessageRecievedEventArgs(this.ConvertType(logInfo[1]), logInfo[0]));
                    }
                }));
                
                
            } else
            {
                if((int)m.commandID == (int)CommandEnum.AddLogCommand)
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        this.addNewLog(m);
                    }));
                }
            }

       
        }

        private void addNewLog(MsgCommand msg)
        {
            MessageRecievedEventArgs m = MessageRecievedEventArgs.FromJSON(msg.args[0]);
            this.m_Logs.Add(m);
        }

        private MessageTypeEnum ConvertType(string type)
        {
            if (type.Equals("INFO")) {
                return MessageTypeEnum.INFO;
            }
            if (type.Equals("FAIL")) {
                return MessageTypeEnum.FAIL;
            }
            else 
            {
                return MessageTypeEnum.WARNING;
            }
            

        }

    }
}

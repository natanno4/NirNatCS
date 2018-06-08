using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Communication;
using Infrastructure;
using ImageService.Infrastructure.Enums;
using Newtonsoft.Json;
using ImageService.Logging.Modal;
using System.Collections.ObjectModel;

namespace ImageServiceWebApp.Models
{
    public class LogsModel
    {

        private IClient m_client;

        public LogsModel()
        {
            Logs = new List<Log>();
            Logs.Add(new Log(0, "0000"));
            Logs.Add(new Log(1, "00000"));
            Logs.Add(new Log(2, "0000"));
            Logs.Add(new Log(0, "111"));
            LogsFilter = new List<Log>();
            m_client = GuiClient.instanceS;
            m_client.CommandRecived += GetInfoFromServer;
            string[] args = new string[5];
            MsgCommand cmd = new MsgCommand((int)CommandEnum.GetConfigCommand, args);
            //get config information
            this.m_client.SendAndRecived(cmd);

        }


        [Required]
        public string TypeChose { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public List<Log> LogsFilter { get; set; }


        public List<Log> Logs { get; set; }

        public void GetInfoFromServer(object sender, MsgCommand msg)
        {
            if (msg.commandID == (int)CommandEnum.LogCommand)
            {
                ObservableCollection<string> collection = JsonConvert.
                    DeserializeObject<ObservableCollection<string>>(msg.args[0]);

                //add logs
                foreach (string log in collection)
                {
                    string[] logInfo = log.Split(';');
                    this.Logs.Add(new Log(Int32.Parse(logInfo[1]), logInfo[0]));
                }
            }
            else if (msg.commandID == (int)CommandEnum.AddLogCommand)
            {
                this.addLog(msg);
            }
        }
        public void addLog(MsgCommand m)
        {

            MessageRecievedEventArgs ms = MessageRecievedEventArgs.FromJSON(m.args[0]);
            Log lg = new Log((int)ms.Status, ms.Message);
            Logs.Add(lg);
        }
    }
}
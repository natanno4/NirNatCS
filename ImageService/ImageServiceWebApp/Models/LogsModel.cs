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
            LogsFilter = new List<Log>();
            m_client = GuiClient.instanceS;
            m_client.CommandRecived += GetInfoFromServer;
            string[] args = new string[5];
            MsgCommand cmd = new MsgCommand((int)CommandEnum.LogCommand, args);
            //get config information
            this.m_client.Write(cmd);
        }


        [Required]
        public string TypeChose { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public List<Log> LogsFilter { get; set; }


        public List<Log> Logs { get; set; }

        /// <summary>
        /// GetInfoFromServer.
        /// a function that is called by an event, and contains
        /// info from server. check if is a Log command or AddLog and act accorindgly.
        /// </summary>
        /// <param name="sender">server</param>
        /// <param name="msg">contains command type and args</param>
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
                    this.Logs.Add(new Log((logInfo[1]), logInfo[0]));
                }
            }
            else if (msg.commandID == (int)CommandEnum.AddLogCommand)
            {
                this.addLog(msg);
            }
        }
        /// <summary>
        /// addLog.
        /// receive a MsgCommand, extract from it all the info about 
        /// the message, create appropriate log and add it to list.
        /// </summary>
        /// <param name="m"></param>
        public void addLog(MsgCommand m)
        {
            MessageRecievedEventArgs cmd = MessageRecievedEventArgs.FromJSON(m.args[0]);
            Log lg = new Log(Log.ConverToString((int) cmd.Status), cmd.Message);
            Logs.Add(lg);
            if(TypeChose != null)
            {
                if(TypeChose.Equals(lg.Type))
                {
                    LogsFilter.Add(lg);
                }
            }
        }
    }
}
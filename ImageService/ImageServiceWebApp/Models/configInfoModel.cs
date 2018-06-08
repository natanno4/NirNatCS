using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Communication;
using Infrastructure;
using ImageService.Infrastructure.Enums;
namespace ImageServiceWebApp.Models
{
    public class ConfigInfoModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "OutPut Directory:")]
        public string OutPutDir { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "SourceName:")]
        public string SourceName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "LogName:")]
        public string LogName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Thumnail size:")]
        public string Thumbnail { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Handlers:")]
        public List<string> Handlers { get; set; }

        [Required]
        public string HandlerRemove { get; set; }

        private IClient m_client;
        private static ConfigInfoModel instance;

        public static ConfigInfoModel SingeltonConfig
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConfigInfoModel();
                }
                return instance;
            }
        }
        private ConfigInfoModel()
        {
            Handlers = new List<string>();
            Handlers.Add("check");
            this.canBeRemoved = false;
            this.HandlerRemove = "";
            this.m_client = GuiClient.instanceS;
            m_client.CommandRecived += InfoFromServer;
            string[] args = new string[3];
            MsgCommand cmd = new MsgCommand((int)CommandEnum.GetConfigCommand,args);
            //get config information
            this.m_client.SendAndRecived(cmd);
            if (!this.m_client.IsConnected())
            {
                //no connection
                this.defaultConfig();
            }
        }

        public void addHandler(string handler)
        {
            Handlers.Add(handler);
        }

        public void InfoFromServer(object sender, MsgCommand msg)
        {
            int id = msg.commandID;
            if (msg.commandID == (int)CommandEnum.GetConfigCommand)
            {
                this.SetConfig(msg);
            }
            else if (msg.commandID == (int)CommandEnum.RemoveHandlerCommand)
            {
                this.canBeRemoved = RemoveHandler(msg);
            }
        }
        public void SetConfig(MsgCommand mesg)
        {
            string[] arg = mesg.args;
            OutPutDir = arg[0];
            SourceName = arg[1];
            LogName = arg[2];
            Thumbnail = arg[3];
            string[] h = mesg.args[4].Split(';');
            foreach (string handler in h)
            {
                if (!Handlers.Contains(handler))
                {
                    Handlers.Add(handler);
                }

            }
        }

        public bool canBeRemoved { get; set; }

        public void RemoveAction()
        {
            string[] args = new string[2];
            args[0] = this.HandlerRemove;
            MsgCommand cmd = new MsgCommand((int)CommandEnum.RemoveHandlerCommand, args);
            this.m_client.SendAndRecived(cmd);
        }

        public bool RemoveHandler(MsgCommand mesg)
        {
            string[] args = mesg.args;
            string rem = args[0];
            if(rem == null)
            {
                this.HandlerRemove = "";
                return false;
            }
            foreach (string s in Handlers)
            {
                if (s.Equals(rem))
                {
                    Handlers.Remove(s);
                    this.HandlerRemove = "";
                    return true;
                }
            }
            return false;
        }

        private void defaultConfig()
        {
            this.OutPutDir = "NO connection";
            this.SourceName = "NO connection";
            this.LogName = "NO connection";
            this.Thumbnail = "NO connection";
        }
    }
}
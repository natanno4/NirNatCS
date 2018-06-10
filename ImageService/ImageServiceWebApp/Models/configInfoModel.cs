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
        // singelton
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
        /// <summary>
        /// add handler to the list.
        /// </summary>
        /// <param name="handler">handler</param>
        public void addHandler(string handler)
        {
            Handlers.Add(handler);
        }

        /// <summary>
        /// InfoFromServer.
        /// a function that is called by an event, and contains
        /// info from server. check if is a config command or remove
        /// handler command and act accorindgly.
        /// </summary>
        /// <param name="sender">server</param>
        /// <param name="msg">contains command type and args</param>
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
        /// <summary>
        /// SetConfig.
        /// set the configuration info that are in msg Command
        /// </summary>
        /// <param name="mesg">MsgCommand</param>
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

        /// <summary>
        /// RemoveAction.
        /// a function that send to server MsgCOmmand that say to remove
        /// handler
        /// </summary>
        public void RemoveAction()
        {
            string[] args = new string[2];
            args[0] = this.HandlerRemove;
            MsgCommand cmd = new MsgCommand((int)CommandEnum.RemoveHandlerCommand, args);
            //this.m_client.SendAndRecived(cmd);
            this.m_client.Write(cmd);
            System.Threading.Thread.Sleep(5);
        }

        /// <summary>
        /// RemoveHandler.
        /// recevieng MsgCommand that include which handler is need
        /// to be remove and does is it.
        /// </summary>
        /// <param name="mesg">MsgCommand</param>
        /// <returns>true if was deleted, false if not appeard</returns>
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
        /// <summary>
        /// defaultConfig.
        /// in case that there isnt connection with server, display
        /// "no connection" in every property of config.
        /// </summary>
        private void defaultConfig()
        {
            this.OutPutDir = "NO connection";
            this.SourceName = "NO connection";
            this.LogName = "NO connection";
            this.Thumbnail = "NO connection";
        }
    }
}
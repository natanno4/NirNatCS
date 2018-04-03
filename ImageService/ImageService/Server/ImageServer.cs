using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Configuration;
using ImageService.Infrastructure.Enums;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Server
{
    public class ImageServer
    {
        #region Members
        private IImageController m_controller;
        private ILoggingService m_logging;

        #endregion
        
        #region Properties
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;          // The event that notifies about a new Command being recieved
        public event EventHandler<DirectoryCloseEventArgs> ServerClose;            // the event that notifies about server closing to relavant handlers
        #endregion
        
        public ImageServer(IImageServiceModal modal, ILoggingService logging)
        {
            this.m_controller = new ImageController(modal);
            this.m_logging = logging;
            string[] pathes = (ConfigurationManager.AppSettings["handler"]).Split(';');
            foreach (string path in pathes)
            {
                this.CreateHandler(path);
            }

        }

        public void CreateHandler(string path)
        {
            IDirectoryHandler handler = new DirectoyHandler(this.m_controller, this.m_logging);
            this.CommandRecieved += handler.OnCommandRecieved;
            this.ServerClose += handler.OnStopHandle;
            handler.StartHandleDirectory(path);
        }
        
        public void CloseServer()
        {
            this.ServerClose?.Invoke(this, new DirectoryCloseEventArgs("path", "server is close"));
            this.m_logging.Log("the server was closed successfully", Logging.Modal.MessageTypeEnum.INFO);
        } 
       
        // add function of send command
        public void SendCommand(CommandRecievedEventArgs e)
        {
            this.CommandRecieved?.Invoke(this, e);
        }
    }
}

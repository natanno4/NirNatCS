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
        // add list of handler, may be changed
        private List<IDirectoryHandler> handlers;
        #region Properties
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;          // The event that notifies about a new Command being recieved
        #endregion
        
        public ImageServer(IImageServiceModal modal, ILoggingService logging)
        {
            this.m_controller = new ImageController(modal);
            this.m_logging = logging;
            this.handlers = new List<IDirectoryHandler>();

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
            handler.StartHandleDirectory(path);
            this.handlers.Add(handler);
        }
        
        public void CloseServer()
        {
            foreach (IDirectoryHandler h in this.handlers)
            {
                h.OnStopHandle(this, new DirectoryCloseEventArgs("path", "directory closed successfully"));
            }
            if (this.handlers.Count == 0)
            this.m_logging.Log("the service is about to shutdown", Logging.Modal.MessageTypeEnum.INFO);
        } 
       
        // add function of send command
        public void SendCommand(CommandRecievedEventArgs e)
        {
            this.CommandRecieved?.Invoke(this, e);
        }
    }
}

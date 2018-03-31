﻿using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using System;
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
        #endregion

        public ImageServer(IImageServiceModal modal, ILoggingService logging, string [] pathes)
        {
            this.m_controller = new ImageController(modal);
            this.m_logging = logging;
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
        }
        public void CloseHandler(object sender, DirectoryCloseEventArgs e)
        {
            IDirectoryHandler temp = (IDirectoryHandler)sender;
            this.CommandRecieved -= temp.OnCommandRecieved;
        }
        public void CloseServer()
        {
            
        } 
       

    }
}

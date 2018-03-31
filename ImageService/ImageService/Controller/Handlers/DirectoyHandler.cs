using ImageService.Modal;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using System.Text.RegularExpressions;

namespace ImageService.Controller.Handlers
{
    public class DirectoyHandler : IDirectoryHandler
    {
        #region Members
        private IImageController m_controller;              // The Image Processing Controller
        private ILoggingService m_logging;
        private FileSystemWatcher m_dirWatcher;             // The Watcher of the Dir
        private string m_path;                              // The Path of directory
        #endregion

        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;   // The Event That Notifies that the Directory is being closed
        public event EventHandler<CommandRecievedEventArgs> createFile;

        public DirectoyHandler(ImageController m_controller, ILoggingService m_logging) {
            this.m_controller = m_controller;
            this.m_logging = m_logging;
            this.m_dirWatcher = new FileSystemWatcher();
        }

        public void StartHandleDirectory(string dirPath) {
            this.m_path = dirPath;
            this.m_dirWatcher.Path = dirPath;
            this.m_dirWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.FileName |
                    NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.LastWrite |
                    NotifyFilters.FileName;
            this.m_dirWatcher.Filter = "*.jpj";
            this.m_dirWatcher.Filter = "*.png";
            this.m_dirWatcher.Filter = "*.gif";
            this.m_dirWatcher.Filter = "*.bmp";
            this.m_dirWatcher.Created += new 

            this.m_dirWatcher.EnableRaisingEvents = true;

        }
        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e)
        {
            Boolean result = true;
            string message;
            if (e.RequestDirPath.Equals(this.m_path)) {
               message = this.m_controller.ExecuteCommand(e.CommandID, e.Args, out result);
               if (result)
                {
                    this.m_logging.Log(message, MessageTypeEnum.INFO);
                } else
                {
                    this.m_logging.Log(message, MessageTypeEnum.FAIL);
                }
            }
           
        }

        public void createNewFile(object sender, CommandRecievedEventArgs e)
        {
            e.CommandID = (int) CommandEnum.NewFileCommand;
            this.OnCommandRecieved(this, e);
        }

        public void closeFile(object sender, DirectoryCloseEventArgs e)
        {

        } 

        // Implement Here!
    }
}

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
using ImageService.Server;
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

        
  
		// Implement Here!

        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;   // The Event That Notifies that the Directory is being closed
        public event EventHandler<CommandRecievedEventArgs> createFile;

        public DirectoyHandler(IImageController m_controller, ILoggingService m_logging) {
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
            this.m_dirWatcher.Filter = "*.*";
            this.m_dirWatcher.Created += new FileSystemEventHandler(CreateNewFile);

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

        public void CreateNewFile(object sender, FileSystemEventArgs e)
        {
            FileInfo file = new FileInfo(e.FullPath);
            if (!(file.Extension.Equals("*.jpg") || file.Extension.Equals("*.png") || file.Extension.Equals("*.gif") ||
                file.Extension.Equals("*.bmp")))
            {
                return;
            }
            string[] args = { e.FullPath, e.Name, };
            CommandRecievedEventArgs ce = new CommandRecievedEventArgs((int)CommandEnum.NewFileCommand,
                args, e.FullPath);
            this.OnCommandRecieved(this, ce);
        }

        public void StopHandle(object sender, DirectoryCloseEventArgs e)
        {
            
            ((ImageServer)sender).CommandRecieved -= this.OnCommandRecieved;
            this.m_dirWatcher.EnableRaisingEvents = false;
            this.m_logging.Log("handler" + this.m_path + "was closed successfully", MessageTypeEnum.INFO);
        } 

        // Implement Here!

    }
}

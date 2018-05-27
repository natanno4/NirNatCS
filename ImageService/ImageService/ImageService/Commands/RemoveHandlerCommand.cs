using ImageService.Commands;
using ImageService.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace ImageService.Commands
{
    class RemoveHandlerCommand : ICommand
    {
        public string Execute(string[] args, out bool result)
        {
            bool flag = false;
            string save = null;
            string temp = ConfigurationManager.AppSettings["Handler"];
            string []handlers = temp.Split(';');
            List<string> copy = new List<string>(handlers);
            foreach (string h in handlers)
            {
                if (h.Equals(args[0]))
                {
                    copy.Remove(h);
                    flag = true;
                    save = h;
                    break;
                }
            }
            if (flag)
            {
                string[] removeHandler = {save}; 
                MsgCommand msg = new MsgCommand((int)CommandEnum.RemoveHandlerCommand, removeHandler);
                createNewHandler(copy); 
                result = true;
                return msg.ToJSON();
            } 
            else
            {
                //log
                result = false;
                return null;
            }
            
        }

        private void createNewHandler(List<string> handlers)
        {
            string handler = null;
            bool first = true;
            foreach (string h in handlers)
            {
                if (first)
                {
                    handler = h;
                    first = false;
                    continue;
                }
                handler = handler + ";" + h;
            }
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove("Handler");
            config.AppSettings.Settings.Add("Handler", handler);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

    }
}

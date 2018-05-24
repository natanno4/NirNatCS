using ImageService.Logging.Modal;
using ImageServiceGUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ViewModel;

namespace ImageServiceGUI.ViewModel
{
    class LogViewModel : VModel
    {
        private ILogModel LogVM;

        public LogViewModel()
        {
            LogVM = new LogModel();
            this.LogVM.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };
        }
        
        public ObservableCollection<string> Logs
        {
            get
            {
                return this.LogVM.Logs;
            }
        }
    }
}

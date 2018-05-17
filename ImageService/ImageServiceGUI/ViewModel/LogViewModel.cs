using System;
using System.Collections.Generic;
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
        public event PropertyChangedEventArgs PropertyChanged;

        public LogViewModel()
        {
            LogVM = new LogModel();
            this.LogVM.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };
        }

        private void NotifyPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        
        public ObservableCollection<MessageRecievedEventArgs> Logs
        {
            get
            {
                return this.LogVM.Logs;
            }
        }
    }
}

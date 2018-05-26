
using ImageServiceGUI.Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel;

namespace ImageServiceGUI.ViewModel
{
    public class MainWindowViewModel : VModel
    {
        private MainWindowModel model;
        public ICommand Close { get; private set; }
        private ICommand closeCommand;


        public MainWindowViewModel()
        {
            this.model = new MainWindowModel();
            this.closeCommand = new DelegateCommand<object>(this.OnClose, this.canBeClosed);
            this.model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };
        }

        public bool canBeClosed(Object obj)
        {
            return true;
        }

        public void OnClose(Object obj)
        {
            this.model.OnClose();
        }


        public bool IsConnected
        {
            get
            {
                return this.model.IsConnected;
            }

            set
            {
                this.model.IsConnected = value;
                NotifyPropertyChanged("IsConnected");
            }
        }
    }
}

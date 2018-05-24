using ImageService.Commands;
using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Prism.Commands;
using Communication.Event;
using ImageService.Infrastructure.Enums;

namespace ViewModel
{
    public class SettingsVM : VModel
    {
        private ISettingsModel  model;
       

        public SettingsVM()
        {
            this.model = new SettingsModel();
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs p)
            {
                NotifyPropertyChanged(p.PropertyName);
            };
            this.remove = new DelegateCommand<object>(this.onRemove, this.canBeRemoved);
        }

        public string OutPutDir
        {
            get { return model.outPut; }
            set
            {
                model.outPut = value;
                NotifyPropertyChanged("OutPutDir");
            }
        }

        public string SourceName
        {
            get { return model.sourceNmae; }
            set
            {
                model.sourceNmae = value;
                NotifyPropertyChanged("SourceName");
            }
        }

        public string LogName
        {
            get { return model.logName; }
            set
            {
                model.logName = value;
                NotifyPropertyChanged("LogName");
            }
        }

        public string TumbNail
        {
            get { return model.thumbNail; }
            set
            {
                model.thumbNail = value;
                NotifyPropertyChanged("TumbNail");
            }
        }

        public ObservableCollection<string> handlers
        {
            get
            {
                return model.handlers;
            }
            set
            {
                model.handlers = value;
                NotifyPropertyChanged("handlers");
            }
        }

        public string selectedHandler
        {
            get
            {
                return this.model.selectedHandler;
            }
            set
            {
                this.model.selectedHandler = value;
                var command = remove as DelegateCommand<object>;
                command.RaiseCanExecuteChanged();
                NotifyPropertyChanged("selectedHandler");
            }
        }
        public ICommand remove { get; private set; }



        private bool canBeRemoved(object obj)
        {
            if (this.model.selectedHandler != null)
            {
                return true;
            }
            return false;
        }

        private void onRemove(object obj)
        {
            string[] arr = { this.selectedHandler };
            this.model.sendCommand(new CommandRecievedEventArgs((int)CommandEnum.RemoveHandlerCommand, arr,
                null));
        }







    }
}
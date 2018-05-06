using Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ViewModel
{
    public class SettingsVM : ViewModel
    {
        private ISettingsModel  model;
        public SettingsVM()
        {
            this.model = new SettingsModel();
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs p)
            {
                NotifyPropertyChanged(p.PropertyName);
            };  
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

    }
}
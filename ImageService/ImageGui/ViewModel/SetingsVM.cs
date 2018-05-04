using System;

namespace ViewModel
{
    public class SettingsVM : ViewModel
    {
        private ISettingsModel  model;
        public SettingsVM(ISettingsModel modell)
        {
            this.model = modell;
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
                NotifyPropertyChanged("LogNmae");
            }
        }

        public int TumbNail
        {
            get { return model.thumbNail; }
            set
            {
                model.thumbNail = value;
                NotifyPropertyChanged("LogNmae");
            }
        }

        public void SaveChanges()
        {
            //model.SaveSettings();
        }
    }
}
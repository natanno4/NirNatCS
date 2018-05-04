using System;
using System.ComponentModel;

namespace Model {
    public class SettingsModel : ISettingsModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string saveLogName;
        private string OutPutDir;
        private string saveSourceName;
        private int tumbNailSize;
        public void NotifyPropertyChanged(string propname)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }

        public SettingsModel()
        {
        }

       
        public string logName
        {
            get
            {
                return this.saveLogName;
            }

            set
            {
                saveLogName = value;
                NotifyPropertyChanged("logName");
            }
        }

        public string outPut
        {
            get
            {
                return this.OutPutDir;
            }

            set
            {
                OutPutDir = value;
                NotifyPropertyChanged("outPut");
            }
        }

        public string sourceNmae
        {
            get
            {
                return this.saveSourceName;
            }

            set
            {
                this.saveSourceName = value;
                NotifyPropertyChanged("sourceNmae");
            }
        }

        public int thumbNail
        {
            get
            {
                return this.tumbNailSize;
            }

            set
            {
                this.tumbNailSize = value;
                NotifyPropertyChanged("thumbNail");
            }
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

public interface ISettingsModel : INotifyPropertyChanged
{
    string outPut { get; set; }
    string sourceNmae { get; set; }
    string logName { get; set; }
    string thumbNail { get; set; }
    ObservableCollection<string> handlers { get; set; }

    //void SaveSettings();

}

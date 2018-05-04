using System;
using System.ComponentModel;

public interface ISettingsModel : INotifyPropertyChanged
{
    string outPut { get; set; }
    string sourceNmae { get; set; }
    string logName { get; set; }
    int thumbNail { get; set; }

    //void SaveSettings();

}

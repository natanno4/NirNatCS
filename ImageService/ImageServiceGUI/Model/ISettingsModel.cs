using Communication;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Communication.Event;
using Infrastructure;

public interface ISettingsModel : INotifyPropertyChanged
{
    string outPut { get; set; }
    string sourceNmae { get; set; }
    string logName { get; set; }
    string thumbNail { get; set; }
    string selectedHandler { get; set; }
    ObservableCollection<string> handlers { get; set; }
    void sendCommand(MsgCommand msg);
}



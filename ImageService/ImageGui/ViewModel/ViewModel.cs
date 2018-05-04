using System;
using System.ComponentModel;

public class ViewModel : INotifypropertyChanged 
{
    public event PropertyChangedEventHandler propertyChanged;

    public void NotifyPropertyChanged (string propname)
    {
        this.propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
    }

}

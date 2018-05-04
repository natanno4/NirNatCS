using System;
using System.ComponentModel;

namespace ViewModel
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propname)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }

    }
}

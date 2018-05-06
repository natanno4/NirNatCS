using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;

namespace ImageGui.View
{
    class SettingsView : MainWindow
    {
        private SettingsVM svm;

        public SettingsView()
        {
            InitializeComponent();
            this.svm = new SettingsVM();
            this.DataContext = svm;
        }

    }
}

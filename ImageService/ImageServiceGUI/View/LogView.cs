using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageServiceGUI;
using ViewModel;
using ImageServiceGUI.ViewModel;

namespace ImageServiceGUI.View
{
    class LogView : MainWindow
    {

        private VModel vm;
        
        public LogView()
        {
            InitializeComponent();
            vm = new LogViewModel();
            this.DataContext = vm;
        }

    }
}

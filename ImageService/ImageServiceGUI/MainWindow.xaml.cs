using ImageServiceGUI.ViewModel;
using ImageServiceGUI.View;
using System.Windows;


namespace ImageServiceGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MainWindowViewModel mvm;
        public MainWindow()
        {
            InitializeComponent();
            this.mvm = new MainWindowViewModel();
            this.DataContext = mvm;
        }
    }
}

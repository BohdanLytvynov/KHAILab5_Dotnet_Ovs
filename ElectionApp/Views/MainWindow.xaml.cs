using ElectionApp.ViewModels;
using System.Windows;

namespace ElectionApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel m_vm;

        public MainWindow()
        {
            InitializeComponent();

            m_vm = new MainWindowViewModel();

            this.DataContext = m_vm;

            m_vm.Dispatcher = this.Dispatcher;
        }
    }
}

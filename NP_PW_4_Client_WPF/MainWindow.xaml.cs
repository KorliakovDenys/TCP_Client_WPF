using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NP_PW_4_Client_WPF{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window{
        private readonly ViewModel _viewModel = new();

        public MainWindow(){
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
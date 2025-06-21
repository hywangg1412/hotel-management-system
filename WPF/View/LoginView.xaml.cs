using System.Windows.Controls;
using WPF.ViewModel;

namespace WPF.View
{
    public partial class LoginView : UserControl
    {
        public LoginView(ViewModel.MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            DataContext = new ViewModel.LoginViewModel(mainWindowViewModel);
        }
        public LoginView()
        {
            InitializeComponent();
        }
    }
}

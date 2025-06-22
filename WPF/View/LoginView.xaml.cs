using System.Windows.Controls;
using WPF.ViewModel;
using System.Windows;

namespace WPF.View
{
    public partial class LoginView : UserControl
    {
        public LoginView(ViewModel.MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            DataContext = new ViewModel.LoginViewModel(mainWindowViewModel);
            
            // Handle PasswordBox binding
            PasswordBox.PasswordChanged += (sender, e) =>
            {
                if (DataContext is LoginViewModel viewModel)
                {
                    viewModel.Password = PasswordBox.Password;
                }
            };
        }
    }
}

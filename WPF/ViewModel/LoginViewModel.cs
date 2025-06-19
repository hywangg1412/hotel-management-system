using Bussiness;
using DataAccess.Context.Common;
using System.Windows.Input;
using WPF.Commands;

namespace WPF.ViewModel
{
    public class LoginViewModel
    {
        private readonly CustomerService _customerService;
        private string _email;
        private string _password;
        private string _loginResult;

        private MainWindowViewModel _mainWindowViewModel;
        public LoginViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            LoginCommand = new RelayCommand(_ => LoginCommand());
            LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string LoginResult
        {
            get => _loginResult;
            set
            {
                _loginResult = value;
                OnPropertyChanged(nameof(LoginResult));
            }
        }

        public ICommand LoginCommand { get; private set; }

        private bool CanExecuteLogin(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);
        }

        private void ExecuteLogin(object parameter)
        {
            try
            {
                var user = _customerService.Login(Email, Password);
                if (user != null)
                {
                    LoginResult = "Login successful!";
                    // TODO: Navigate to main window
                }
                else
                {
                    LoginResult = "Invalid email or password";
                }
            }
            catch (Exception ex)
            {
                LoginResult = "An error occurred during login";
                AppLogger.LogError($"Log error: {ex.Message}");
            }
        }
    }
}

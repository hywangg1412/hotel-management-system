using Bussiness;
using Common;
using DataAccess.Context.Common;
using System.ComponentModel;
using System.Windows.Input;
using WPF.Commands;

namespace WPF.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly CustomerService _customerService;
        private string _email;
        private string _password;
        private string _loginResult;

        private MainWindowViewModel _mainWindowViewModel;
        public LoginViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _customerService = new CustomerService();
            LoginCommand = new RelayCommand(_ => Login());
            SignupCommand = new RelayCommand(_ => _mainWindowViewModel.ShowSignupView());

            // Debug: Check if AppConfig is working
            try
            {
                AppLogger.LogInformation($"AppConfig loaded - Admin Email: {AppConfig.AdminEmail}");
                AppLogger.LogInformation($"AppConfig loaded - User Email: {AppConfig.UserEmail}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError($"Error loading AppConfig: {ex.Message}");
            }
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

        public ICommand LoginCommand { get; }
        public ICommand SignupCommand { get; }

        private void Login()
        {
            try
            {
                // Debug logging
                AppLogger.LogInformation($"Login attempt - Email: {Email}");
                AppLogger.LogInformation($"Admin Email from config: {AppConfig.AdminEmail}");
                AppLogger.LogInformation($"User Email from config: {AppConfig.UserEmail}");

                if (Email == AppConfig.AdminEmail && Password == AppConfig.AdminPassword)
                {
                    LoginResult = "Admin login successful!";
                    _mainWindowViewModel.ShowAdminDashboard();
                }
                else
                {
                    var customer = _customerService.Login(Email, Password);
                    if (customer != null)
                    {
                        LoginResult = "Customer login successful!";
                        _mainWindowViewModel.ShowCustomerDashboard(customer);
                    }
                    else
                    {
                        LoginResult = "Invalid email or password!";
                    }
                }
                OnPropertyChanged(nameof(LoginResult));
            }
            catch (Exception ex)
            {
                LoginResult = "An error occurred during login";
                AppLogger.LogError($"Login error: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

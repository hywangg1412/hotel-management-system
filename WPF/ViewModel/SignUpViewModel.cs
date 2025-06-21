using Bussiness;
using Models;
using System.ComponentModel;
using System.Windows.Input;
using WPF.Commands;

namespace WPF.ViewModel
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        private readonly CustomerService _customerService;
        private readonly MainWindowViewModel _mainWindowViewModel;

        private string _fullName;
        private string _telephone;
        private string _email;
        private DateOnly _birthday = DateOnly.FromDateTime(DateTime.Today);
        private string _password;
        private string _confirmPassword;
        private string _message;

        public SignUpViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _customerService = new CustomerService();
            
            SignupCommand = new RelayCommand(_ => Signup(), _ => CanSignup());
            BackToLoginCommand = new RelayCommand(_ => _mainWindowViewModel.ShowLoginView());
        }

        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string Telephone
        {
            get => _telephone;
            set
            {
                _telephone = value;
                OnPropertyChanged(nameof(Telephone));
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

        public DateOnly Birthday
        {
            get => _birthday;
            set
            {
                _birthday = value;
                OnPropertyChanged(nameof(Birthday));
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

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public ICommand SignupCommand { get; }
        public ICommand BackToLoginCommand { get; }

        private bool CanSignup()
        {
            return !string.IsNullOrWhiteSpace(FullName) &&
                   !string.IsNullOrWhiteSpace(Telephone) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   !string.IsNullOrWhiteSpace(Password) &&
                   Password == ConfirmPassword;
        }

        private void Signup()
        {
            try
            {
                if (Password != ConfirmPassword)
                {
                    Message = "Passwords do not match!";
                    return;
                }

                // Check if email already exists
                var existingCustomers = _customerService.GetAll();
                if (existingCustomers.Any(c => c.Email.Equals(Email, StringComparison.OrdinalIgnoreCase)))
                {
                    Message = "Email already exists! Please use a different email.";
                    return;
                }

                // Create new customer
                var customer = new Customer
                {
                    FullName = FullName,
                    Telephone = Telephone,
                    Email = Email,
                    Birthday = Birthday,
                    Password = Password,
                    Status = 1 // Active
                };

                _customerService.Add(customer);
                Message = "Account created successfully! Please login with your new credentials.";
                
                // Clear form
                FullName = string.Empty;
                Telephone = string.Empty;
                Email = string.Empty;
                Birthday = DateOnly.FromDateTime(DateTime.Today);
                Password = string.Empty;
                ConfirmPassword = string.Empty;
            }
            catch (Exception ex)
            {
                Message = $"Error creating account: {ex.Message}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

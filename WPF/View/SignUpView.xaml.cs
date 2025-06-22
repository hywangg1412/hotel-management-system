using System.Windows.Controls;
using WPF.ViewModel;

namespace WPF.View
{
    public partial class SignUpView : UserControl
    {
        private readonly SignUpViewModel _signupViewModel;

        public SignUpView(MainWindowViewModel mainVM)
        {
            InitializeComponent();
            _signupViewModel = new SignUpViewModel(mainVM);
            DataContext = _signupViewModel;
            this.Loaded += SignupView_Loaded;
        }

        public SignUpView()
        {
            InitializeComponent();
        }

        private void SignupView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (PasswordBox != null)
            {
                PasswordBox.PasswordChanged += PasswordBox_PasswordChanged;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_signupViewModel != null && PasswordBox != null)
            {
                _signupViewModel.Password = PasswordBox.Password;
            }
        }
    }
}

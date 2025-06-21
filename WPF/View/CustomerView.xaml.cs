using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Models;
using WPF.ViewModel;

namespace WPF.View
{
    /// <summary>
    /// Interaction logic for CustomerView.xaml
    /// </summary>
    public partial class CustomerView : UserControl
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly CustomerViewModel _customerViewModel;

        public CustomerView(MainWindowViewModel mainWindowViewModel, Customer customer)
        {
            InitializeComponent();
            _mainWindowViewModel = mainWindowViewModel;
            _customerViewModel = new CustomerViewModel(customer);
            DataContext = _customerViewModel;

            // Set up password binding
            PasswordBox.PasswordChanged += PasswordBox_PasswordChanged;
            ConfirmPasswordBox.PasswordChanged += ConfirmPasswordBox_PasswordChanged;
        }

        public CustomerView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (_customerViewModel != null)
            {
                _customerViewModel.UpdatePasswordFromPasswordBox(PasswordBox.Password);
            }
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (_customerViewModel != null)
            {
                _customerViewModel.UpdateConfirmPasswordFromPasswordBox(ConfirmPasswordBox.Password);
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to logout?", 
                "Confirm Logout", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if (result == MessageBoxResult.Yes)
            {
                _mainWindowViewModel.ShowLoginView();
            }
        }
    }
}

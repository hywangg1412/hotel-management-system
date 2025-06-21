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
using WPF.ViewModel;

namespace WPF.View
{
    /// <summary>
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : UserControl
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly AdminViewModel _adminViewModel;

        public AdminView(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            _mainWindowViewModel = mainWindowViewModel;
            _adminViewModel = new AdminViewModel();
            DataContext = _adminViewModel;

            // Set up password binding for customer form
            CustomerPasswordBox.PasswordChanged += CustomerPasswordBox_PasswordChanged;
        }

        public AdminView()
        {
            InitializeComponent();
        }

        private void CustomerPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (_adminViewModel != null)
            {
                _adminViewModel.NewCustomerPassword = CustomerPasswordBox.Password;
            }
        }

        // Method to clear password box
        public void ClearPasswordBox()
        {
            CustomerPasswordBox.Password = string.Empty;
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

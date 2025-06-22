using Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WPF.View;

namespace WPF.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(); }
        }

        public MainWindowViewModel()
        {
            CurrentView = new MenuView(this);
        }

        public void ShowLoginView()
        {
            CurrentView = new LoginView(this);
        }

        public void ShowSignupView()
        {
            CurrentView = new SignUpView(this);
        }

        public void ShowAdminDashboard()
        {
            CurrentView = new AdminView(this);
        }

        public void ShowCustomerDashboard(Customer customer)
        {
            CurrentView = new CustomerView(this, customer);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

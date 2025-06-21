using System.Windows.Input;
using WPF.Commands;

namespace WPF.ViewModel
{
    public class MenuViewModel
    {
        public ICommand LoginCommand { get; set; }
        public ICommand SignUpCommand { get; set; }

        private MainWindowViewModel _mainWindowViewModel;

        public MenuViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            LoginCommand = new RelayCommand(_ => mainWindowViewModel.ShowLoginView());
            SignUpCommand = new RelayCommand(_ => mainWindowViewModel.ShowSignupView());
        }
    }
}

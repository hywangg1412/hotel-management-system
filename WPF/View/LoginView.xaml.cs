using System.Windows.Controls;
using WPF.ViewModel;

namespace WPF.View
{
    public partial class LoginView : UserControl
    {
        public LoginView(MainWindowViewModel mainVM)
        {
            InitializeComponent();
            DataContext = new LoginViewModel(mainVM);
        }
    }
}

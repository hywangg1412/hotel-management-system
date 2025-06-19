using System.Windows.Controls;
using WPF.ViewModel;

namespace WPF.View
{
    public partial class SignUpView : UserControl
    {
        public SignUpView(MainWindowViewModel mainVM)
        {
            InitializeComponent();
            DataContext = new SignUpViewModel(mainVM);
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }
    }
}

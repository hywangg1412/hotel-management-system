using System.Windows.Controls;
using WPF.ViewModel;

namespace WPF.View
{
    public partial class MenuView : UserControl
    {
        public MenuView(MainWindowViewModel mainVM)
        {
            InitializeComponent();
            DataContext = new MenuViewModel(mainVM);
        }
        public MenuView()
        {
            InitializeComponent();
        }
    }
}

using System.Windows;

namespace WPF.View
{
    public partial class RoomDialog : Window
    {
        public RoomDialog()
        {
            InitializeComponent();
            
            // Subscribe to DialogResult changes
            this.DataContextChanged += RoomDialog_DataContextChanged;
        }

        private void RoomDialog_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is ViewModel.RoomDialogViewModel viewModel)
            {
                viewModel.PropertyChanged += ViewModel_PropertyChanged;
            }
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DialogResult" && sender is ViewModel.RoomDialogViewModel viewModel)
            {
                if (viewModel.DialogResult.HasValue)
                {
                    this.DialogResult = viewModel.DialogResult;
                    this.Close();
                }
            }
        }
    }
} 
using System.Windows;

namespace WPF.View
{
    public partial class CustomerDialog : Window
    {
        public CustomerDialog()
        {
            InitializeComponent();
            
            // Subscribe to DialogResult changes
            this.DataContextChanged += CustomerDialog_DataContextChanged;
        }

        private void CustomerDialog_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is ViewModel.CustomerDialogViewModel viewModel)
            {
                viewModel.PropertyChanged += ViewModel_PropertyChanged;
            }
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DialogResult" && sender is ViewModel.CustomerDialogViewModel viewModel)
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
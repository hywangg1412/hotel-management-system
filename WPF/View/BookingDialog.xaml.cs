using System.Windows;

namespace WPF.View
{
    public partial class BookingDialog : Window
    {
        public BookingDialog()
        {
            InitializeComponent();
            this.DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is ViewModel.BookingDialogViewModel viewModel)
            {
                viewModel.PropertyChanged += (s, args) =>
                {
                    if (args.PropertyName == "DialogResult" && viewModel.DialogResult.HasValue)
                    {
                        this.DialogResult = viewModel.DialogResult;
                        this.Close();
                    }
                };
            }
        }
    }
} 
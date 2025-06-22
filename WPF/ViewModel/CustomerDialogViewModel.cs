using Models;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WPF.Commands;

namespace WPF.ViewModel
{
    public class CustomerDialogViewModel : INotifyPropertyChanged
    {
        private Customer _customer;
        private string _dialogTitle;
        private bool _isEditMode;
        private bool? _dialogResult;

        public CustomerDialogViewModel(Customer customer = null)
        {
            _customer = customer ?? new Customer();
            _isEditMode = customer != null;
            _dialogTitle = _isEditMode ? "Edit Customer" : "Add New Customer";

            SaveCommand = new RelayCommand(_ => Save());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        public Customer Customer
        {
            get => _customer;
            set
            {
                _customer = value;
                OnPropertyChanged(nameof(Customer));
            }
        }

        public string DialogTitle
        {
            get => _dialogTitle;
            set
            {
                _dialogTitle = value;
                OnPropertyChanged(nameof(DialogTitle));
            }
        }

        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                _isEditMode = value;
                OnPropertyChanged(nameof(IsEditMode));
            }
        }

        public bool? DialogResult
        {
            get => _dialogResult;
            set
            {
                _dialogResult = value;
                OnPropertyChanged(nameof(DialogResult));
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(Customer.FullName) || 
                string.IsNullOrWhiteSpace(Customer.Email) ||
                string.IsNullOrWhiteSpace(Customer.Telephone))
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Set default values if not provided
            if (Customer.UserID == 0)
            {
                Customer.UserID = new Random().Next(1000, 9999);
            }
            if (Customer.Status == 0)
            {
                Customer.Status = 1; // Default to active
            }

            DialogResult = true;
        }

        private void Cancel()
        {
            DialogResult = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
} 
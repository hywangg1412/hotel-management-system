using Bussiness;
using Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPF.Commands;

namespace WPF.ViewModel
{
    public class BookingDialogViewModel : INotifyPropertyChanged
    {
        private readonly CustomerService _customerService;
        private readonly RoomService _roomService;

        private Booking _booking;
        private bool? _dialogResult;
        
        public ObservableCollection<Customer> Customers { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }

        public Booking Booking
        {
            get => _booking;
            set { _booking = value; OnPropertyChanged(nameof(Booking)); }
        }
        
        public bool? DialogResult
        {
            get => _dialogResult;
            set { _dialogResult = value; OnPropertyChanged(nameof(DialogResult)); }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public BookingDialogViewModel(Booking booking)
        {
            _customerService = new CustomerService();
            _roomService = new RoomService();
            
            // Create a copy for editing
            Booking = new Booking
            {
                BookingID = booking.BookingID,
                CustomerID = booking.CustomerID,
                RoomID = booking.RoomID,
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                TotalPrice = booking.TotalPrice,
                Status = booking.Status
            };

            LoadCustomersAndRooms();

            SaveCommand = new RelayCommand(_ => Save());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        private void LoadCustomersAndRooms()
        {
            Customers = new ObservableCollection<Customer>(_customerService.GetAll());
            Rooms = new ObservableCollection<Room>(_roomService.GetAll());
        }

        private void Save()
        {
            if (Booking.EndDate <= Booking.StartDate)
            {
                MessageBox.Show("End date must be after start date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Recalculate total price
            var room = Rooms.FirstOrDefault(r => r.RoomId == Booking.RoomID);
            if (room != null)
            {
                var days = (Booking.EndDate - Booking.StartDate).Days;
                Booking.TotalPrice = room.RoomPricePerDate * (days > 0 ? days : 1);
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
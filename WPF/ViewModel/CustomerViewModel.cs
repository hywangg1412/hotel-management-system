using Bussiness;
using Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WPF.Commands;

namespace WPF.ViewModel
{
    public class CustomerViewModel : INotifyPropertyChanged
    {
        private readonly CustomerService _customerService;
        private readonly BookingService _bookingService;
        private readonly RoomService _roomService;
        private readonly RoomTypeService _roomTypeService;

        private Customer _currentCustomer;
        private string _fullName;
        private string _telephone;
        private string _email;
        private DateOnly _birthday;
        private string _password;
        private string _confirmPassword;
        private string _message;

        // Booking properties
        private ObservableCollection<Booking> _bookings;
        private Booking _selectedBooking;
        private DateTime _startDate = DateTime.Today;
        private DateTime _endDate = DateTime.Today.AddDays(1);
        private Room _selectedRoom;
        private ObservableCollection<Room> _availableRooms;

        // Search properties
        private string _searchKeyword;

        public CustomerViewModel(Customer currentCustomer)
        {
            _customerService = new CustomerService();
            _bookingService = new BookingService();
            _roomService = new RoomService();
            _roomTypeService = new RoomTypeService();

            _currentCustomer = currentCustomer;
            LoadCustomerProfile();
            LoadBookings();
            LoadAvailableRooms();

            // Initialize commands
            UpdateProfileCommand = new RelayCommand(_ => UpdateProfile(), _ => CanUpdateProfile());
            CreateBookingCommand = new RelayCommand(_ => CreateBooking(), _ => CanCreateBooking());
            CancelBookingCommand = new RelayCommand(_ => CancelBooking(), _ => CanCancelBooking());
            SearchBookingsCommand = new RelayCommand(_ => SearchBookings());
            RefreshCommand = new RelayCommand(_ => RefreshData());
        }

        #region Customer Profile Properties

        public Customer CurrentCustomer
        {
            get => _currentCustomer;
            set
            {
                _currentCustomer = value;
                OnPropertyChanged(nameof(CurrentCustomer));
            }
        }

        public string FullName
        {
            get => _fullName;
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string Telephone
        {
            get => _telephone;
            set
            {
                _telephone = value;
                OnPropertyChanged(nameof(Telephone));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public DateOnly Birthday
        {
            get => _birthday;
            set
            {
                _birthday = value;
                OnPropertyChanged(nameof(Birthday));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        #endregion

        #region Booking Properties

        public ObservableCollection<Booking> Bookings
        {
            get => _bookings;
            set
            {
                _bookings = value;
                OnPropertyChanged(nameof(Bookings));
            }
        }

        public Booking SelectedBooking
        {
            get => _selectedBooking;
            set
            {
                _selectedBooking = value;
                OnPropertyChanged(nameof(SelectedBooking));
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }

        public ObservableCollection<Room> AvailableRooms
        {
            get => _availableRooms;
            set
            {
                _availableRooms = value;
                OnPropertyChanged(nameof(AvailableRooms));
            }
        }

        #endregion

        #region Search Properties

        public string SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                _searchKeyword = value;
                OnPropertyChanged(nameof(SearchKeyword));
            }
        }

        #endregion

        #region Message Property

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        #endregion

        #region Commands

        public ICommand UpdateProfileCommand { get; }
        public ICommand CreateBookingCommand { get; }
        public ICommand CancelBookingCommand { get; }
        public ICommand SearchBookingsCommand { get; }
        public ICommand RefreshCommand { get; }

        #endregion

        #region Private Methods

        private void LoadCustomerProfile()
        {
            if (_currentCustomer != null)
            {
                FullName = _currentCustomer.FullName;
                Telephone = _currentCustomer.Telephone;
                Email = _currentCustomer.Email;
                Birthday = _currentCustomer.Birthday;
                Password = _currentCustomer.Password;
                ConfirmPassword = _currentCustomer.Password;
            }
        }

        private void LoadBookings()
        {
            try
            {
                var allBookings = _bookingService.GetAll();
                var customerBookings = allBookings.Where(b => b.CustomerID == _currentCustomer.UserID).ToList();
                Bookings = new ObservableCollection<Booking>(customerBookings);
            }
            catch (Exception ex)
            {
                Message = $"Error loading bookings: {ex.Message}";
            }
        }

        private void LoadAvailableRooms()
        {
            try
            {
                var allRooms = _roomService.GetAll();
                var availableRooms = allRooms.Where(r => r.RoomStatus == "1").ToList(); // Active rooms
                AvailableRooms = new ObservableCollection<Room>(availableRooms);
            }
            catch (Exception ex)
            {
                Message = $"Error loading rooms: {ex.Message}";
            }
        }

        private bool CanUpdateProfile()
        {
            return !string.IsNullOrWhiteSpace(FullName) &&
                   !string.IsNullOrWhiteSpace(Telephone) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   !string.IsNullOrWhiteSpace(Password) &&
                   Password == ConfirmPassword;
        }

        private void UpdateProfile()
        {
            try
            {
                // Get password from PasswordBox if available
                if (Password != ConfirmPassword)
                {
                    Message = "Passwords do not match!";
                    return;
                }

                _currentCustomer.FullName = FullName;
                _currentCustomer.Telephone = Telephone;
                _currentCustomer.Email = Email;
                _currentCustomer.Birthday = Birthday;
                _currentCustomer.Password = Password;

                _customerService.Update(_currentCustomer);
                Message = "Profile updated successfully!";
            }
            catch (Exception ex)
            {
                Message = $"Error updating profile: {ex.Message}";
            }
        }

        // Method to update password from PasswordBox
        public void UpdatePasswordFromPasswordBox(string password)
        {
            Password = password;
        }

        public void UpdateConfirmPasswordFromPasswordBox(string confirmPassword)
        {
            ConfirmPassword = confirmPassword;
        }

        private bool CanCreateBooking()
        {
            return SelectedRoom != null &&
                   StartDate >= DateTime.Today &&
                   EndDate > StartDate;
        }

        private void CreateBooking()
        {
            try
            {
                if (SelectedRoom == null)
                {
                    Message = "Please select a room!";
                    return;
                }

                if (StartDate < DateTime.Today)
                {
                    Message = "Start date cannot be in the past!";
                    return;
                }

                if (EndDate <= StartDate)
                {
                    Message = "End date must be after start date!";
                    return;
                }

                var days = (EndDate - StartDate).Days;
                var totalPrice = SelectedRoom.RoomPricePerDate * days;

                var booking = new Booking
                {
                    CustomerID = _currentCustomer.UserID,
                    RoomID = SelectedRoom.RoomId,
                    StartDate = StartDate,
                    EndDate = EndDate,
                    TotalPrice = totalPrice,
                    Status = "Confirmed"
                };

                _bookingService.Add(booking);
                LoadBookings();
                Message = "Booking created successfully!";

                // Reset booking form
                StartDate = DateTime.Today;
                EndDate = DateTime.Today.AddDays(1);
                SelectedRoom = null;
            }
            catch (Exception ex)
            {
                Message = $"Error creating booking: {ex.Message}";
            }
        }

        private bool CanCancelBooking()
        {
            return SelectedBooking != null && SelectedBooking.Status != "Cancelled";
        }

        private void CancelBooking()
        {
            try
            {
                if (SelectedBooking == null)
                {
                    Message = "Please select a booking to cancel!";
                    return;
                }

                var result = MessageBox.Show("Are you sure you want to cancel this booking?",
                    "Confirm Cancellation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    SelectedBooking.Status = "Cancelled";
                    _bookingService.Update(SelectedBooking);
                    LoadBookings();
                    Message = "Booking cancelled successfully!";
                }
            }
            catch (Exception ex)
            {
                Message = $"Error cancelling booking: {ex.Message}";
            }
        }

        private void SearchBookings()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(SearchKeyword))
                {
                    LoadBookings();
                    return;
                }

                var allBookings = _bookingService.GetAll();
                var customerBookings = allBookings.Where(b => b.CustomerID == _currentCustomer.UserID).ToList();

                var filteredBookings = customerBookings.Where(b =>
                    b.Status.Contains(SearchKeyword, StringComparison.OrdinalIgnoreCase) ||
                    b.TotalPrice.ToString().Contains(SearchKeyword) ||
                    b.StartDate.ToString("dd/MM/yyyy").Contains(SearchKeyword) ||
                    b.EndDate.ToString("dd/MM/yyyy").Contains(SearchKeyword)
                ).ToList();

                Bookings = new ObservableCollection<Booking>(filteredBookings);
                Message = $"Found {filteredBookings.Count} bookings matching '{SearchKeyword}'";
            }
            catch (Exception ex)
            {
                Message = $"Error searching bookings: {ex.Message}";
            }
        }

        private void RefreshData()
        {
            LoadCustomerProfile();
            LoadBookings();
            LoadAvailableRooms();
            Message = "Data refreshed successfully!";
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}

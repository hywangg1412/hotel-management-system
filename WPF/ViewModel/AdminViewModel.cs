using Bussiness;
using Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WPF.Commands;
using System.Linq;

namespace WPF.ViewModel
{
    public class AdminViewModel : INotifyPropertyChanged
    {
        private readonly CustomerService _customerService;
        private readonly RoomService _roomService;
        private readonly RoomTypeService _roomTypeService;
        private readonly BookingService _bookingService;

        // Customer Management Properties
        private ObservableCollection<Customer> _customers;
        private Customer _selectedCustomer;
        private string _customerSearchKeyword;

        // Room Management Properties
        private ObservableCollection<Room> _rooms;
        private Room _selectedRoom;
        private string _roomSearchKeyword;
        private ObservableCollection<RoomType> _roomTypes;

        // Booking/Report Properties
        private ObservableCollection<Booking> _bookings;
        private ObservableCollection<Booking> _reportBookings;
        private Booking _selectedBooking;
        private string _bookingSearchKeyword;
        private DateTime _reportStartDate = DateTime.Today.AddDays(-30);
        private DateTime _reportEndDate = DateTime.Today;
        private decimal _totalRevenue;
        private int _totalBookings;

        // Form Properties for Add/Edit
        private string _newCustomerFullName;
        private string _newCustomerTelephone;
        private string _newCustomerEmail;
        private DateOnly _newCustomerBirthday = DateOnly.FromDateTime(DateTime.Today);
        private string _newCustomerPassword;

        private string _newRoomNumber;
        private string _newRoomDescription;
        private int _newRoomMaxCapacity = 1;
        private decimal _newRoomPricePerDate;
        private RoomType _selectedRoomType;

        // Message Property
        private string _message;

        public AdminViewModel()
        {
            _customerService = new CustomerService();
            _roomService = new RoomService();
            _roomTypeService = new RoomTypeService();
            _bookingService = new BookingService();

            LoadData();

            // Initialize Commands
            AddCustomerCommand = new RelayCommand(_ => AddCustomer());
            UpdateCustomerCommand = new RelayCommand(_ => UpdateCustomer(), _ => CanUpdateCustomer());
            DeleteCustomerCommand = new RelayCommand(_ => DeleteCustomer(), _ => CanDeleteCustomer());
            SearchCustomersCommand = new RelayCommand(_ => SearchCustomers());

            AddRoomCommand = new RelayCommand(_ => AddRoom());
            UpdateRoomCommand = new RelayCommand(_ => UpdateRoom(), _ => CanUpdateRoom());
            DeleteRoomCommand = new RelayCommand(_ => DeleteRoom(), _ => CanDeleteRoom());
            SearchRoomsCommand = new RelayCommand(_ => SearchRooms());

            UpdateBookingCommand = new RelayCommand(_ => UpdateBooking(), _ => CanUpdateBooking());
            DeleteBookingCommand = new RelayCommand(_ => DeleteBooking(), _ => CanDeleteBooking());
            SearchBookingsCommand = new RelayCommand(_ => SearchBookings());

            GenerateReportCommand = new RelayCommand(_ => GenerateReport());
            CheckOutCommand = new RelayCommand(_ => CheckOut(), _ => CanCheckOut());
            RefreshDataCommand = new RelayCommand(_ => RefreshData());
        }

        #region Customer Management Properties

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set
            {
                _customers = value;
                OnPropertyChanged(nameof(Customers));
            }
        }

        public Customer SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value;
                OnPropertyChanged(nameof(SelectedCustomer));
                (UpdateCustomerCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (DeleteCustomerCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string CustomerSearchKeyword
        {
            get => _customerSearchKeyword;
            set
            {
                _customerSearchKeyword = value;
                OnPropertyChanged(nameof(CustomerSearchKeyword));
            }
        }

        #endregion

        #region Room Management Properties

        public ObservableCollection<Room> Rooms
        {
            get => _rooms;
            set
            {
                _rooms = value;
                OnPropertyChanged(nameof(Rooms));
            }
        }

        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
                (UpdateRoomCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (DeleteRoomCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string RoomSearchKeyword
        {
            get => _roomSearchKeyword;
            set
            {
                _roomSearchKeyword = value;
                OnPropertyChanged(nameof(RoomSearchKeyword));
            }
        }

        public ObservableCollection<RoomType> RoomTypes
        {
            get => _roomTypes;
            set
            {
                _roomTypes = value;
                OnPropertyChanged(nameof(RoomTypes));
            }
        }

        #endregion

        #region Booking/Report Properties

        public ObservableCollection<Booking> Bookings
        {
            get => _bookings;
            set
            {
                _bookings = value;
                OnPropertyChanged(nameof(Bookings));
            }
        }

        public ObservableCollection<Booking> ReportBookings
        {
            get => _reportBookings;
            set
            {
                _reportBookings = value;
                OnPropertyChanged(nameof(ReportBookings));
            }
        }

        public Booking SelectedBooking
        {
            get => _selectedBooking;
            set
            {
                _selectedBooking = value;
                OnPropertyChanged(nameof(SelectedBooking));
                (CheckOutCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (UpdateBookingCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (DeleteBookingCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string BookingSearchKeyword
        {
            get => _bookingSearchKeyword;
            set
            {
                _bookingSearchKeyword = value;
                OnPropertyChanged(nameof(BookingSearchKeyword));
            }
        }

        public DateTime ReportStartDate
        {
            get => _reportStartDate;
            set
            {
                _reportStartDate = value;
                OnPropertyChanged(nameof(ReportStartDate));
            }
        }

        public DateTime ReportEndDate
        {
            get => _reportEndDate;
            set
            {
                _reportEndDate = value;
                OnPropertyChanged(nameof(ReportEndDate));
            }
        }

        public decimal TotalRevenue
        {
            get => _totalRevenue;
            set
            {
                _totalRevenue = value;
                OnPropertyChanged(nameof(TotalRevenue));
            }
        }

        public int TotalBookings
        {
            get => _totalBookings;
            set
            {
                _totalBookings = value;
                OnPropertyChanged(nameof(TotalBookings));
            }
        }

        #endregion

        #region Form Properties

        public string NewCustomerFullName
        {
            get => _newCustomerFullName;
            set
            {
                _newCustomerFullName = value;
                OnPropertyChanged(nameof(NewCustomerFullName));
            }
        }

        public string NewCustomerTelephone
        {
            get => _newCustomerTelephone;
            set
            {
                _newCustomerTelephone = value;
                OnPropertyChanged(nameof(NewCustomerTelephone));
            }
        }

        public string NewCustomerEmail
        {
            get => _newCustomerEmail;
            set
            {
                _newCustomerEmail = value;
                OnPropertyChanged(nameof(NewCustomerEmail));
            }
        }

        public DateOnly NewCustomerBirthday
        {
            get => _newCustomerBirthday;
            set
            {
                _newCustomerBirthday = value;
                OnPropertyChanged(nameof(NewCustomerBirthday));
            }
        }

        public string NewCustomerPassword
        {
            get => _newCustomerPassword;
            set
            {
                _newCustomerPassword = value;
                OnPropertyChanged(nameof(NewCustomerPassword));
            }
        }

        public string NewRoomNumber
        {
            get => _newRoomNumber;
            set
            {
                _newRoomNumber = value;
                OnPropertyChanged(nameof(NewRoomNumber));
            }
        }

        public string NewRoomDescription
        {
            get => _newRoomDescription;
            set
            {
                _newRoomDescription = value;
                OnPropertyChanged(nameof(NewRoomDescription));
            }
        }

        public int NewRoomMaxCapacity
        {
            get => _newRoomMaxCapacity;
            set
            {
                _newRoomMaxCapacity = value;
                OnPropertyChanged(nameof(NewRoomMaxCapacity));
            }
        }

        public decimal NewRoomPricePerDate
        {
            get => _newRoomPricePerDate;
            set
            {
                _newRoomPricePerDate = value;
                OnPropertyChanged(nameof(NewRoomPricePerDate));
            }
        }

        public RoomType SelectedRoomType
        {
            get => _selectedRoomType;
            set
            {
                _selectedRoomType = value;
                OnPropertyChanged(nameof(SelectedRoomType));
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

        public ICommand AddCustomerCommand { get; }
        public ICommand UpdateCustomerCommand { get; }
        public ICommand DeleteCustomerCommand { get; }
        public ICommand SearchCustomersCommand { get; }

        public ICommand AddRoomCommand { get; }
        public ICommand UpdateRoomCommand { get; }
        public ICommand DeleteRoomCommand { get; }
        public ICommand SearchRoomsCommand { get; }

        public ICommand UpdateBookingCommand { get; }
        public ICommand DeleteBookingCommand { get; }
        public ICommand SearchBookingsCommand { get; }

        public ICommand GenerateReportCommand { get; }
        public ICommand CheckOutCommand { get; }
        public ICommand RefreshDataCommand { get; }

        #endregion

        #region Private Methods

        private void LoadData()
        {
            LoadCustomers();
            LoadRooms();
            LoadRoomTypes();
            LoadBookings();
        }

        private void LoadCustomers()
        {
            try
            {
                var customers = _customerService.GetAll();
                Customers = new ObservableCollection<Customer>(customers);
            }
            catch (Exception ex)
            {
                Message = $"Error loading customers: {ex.Message}";
            }
        }

        private void LoadRooms()
        {
            try
            {
                var rooms = _roomService.GetAll();
                Rooms = new ObservableCollection<Room>(rooms);
            }
            catch (Exception ex)
            {
                Message = $"Error loading rooms: {ex.Message}";
            }
        }

        private void LoadRoomTypes()
        {
            try
            {
                var roomTypes = _roomTypeService.GetAll();
                RoomTypes = new ObservableCollection<RoomType>(roomTypes);
            }
            catch (Exception ex)
            {
                Message = $"Error loading room types: {ex.Message}";
            }
        }

        private void LoadBookings()
        {
            try
            {
                var bookings = _bookingService.GetAll();
                Bookings = new ObservableCollection<Booking>(bookings);
            }
            catch (Exception ex)
            {
                Message = $"Error loading bookings: {ex.Message}";
            }
        }

        private void AddCustomer()
        {
            try
            {
                var dialog = new View.CustomerDialog();
                var viewModel = new CustomerDialogViewModel();
                dialog.DataContext = viewModel;
                dialog.Owner = Application.Current.MainWindow;

                dialog.ShowDialog();
                
                if (viewModel.DialogResult == true)
                {
                    _customerService.Add(viewModel.Customer);
                    LoadCustomers();
                    Message = "Customer added successfully!";
                }
            }
            catch (Exception ex)
            {
                Message = $"Error adding customer: {ex.Message}";
            }
        }

        private bool CanUpdateCustomer()
        {
            return SelectedCustomer != null;
        }

        private void UpdateCustomer()
        {
            try
            {
                if (SelectedCustomer == null) return;

                var dialog = new View.CustomerDialog();
                var viewModel = new CustomerDialogViewModel(SelectedCustomer);
                dialog.DataContext = viewModel;
                dialog.Owner = Application.Current.MainWindow;

                dialog.ShowDialog();
                
                if (viewModel.DialogResult == true)
                {
                    _customerService.Update(viewModel.Customer);
                    LoadCustomers();
                    Message = "Customer updated successfully!";
                }
            }
            catch (Exception ex)
            {
                Message = $"Error updating customer: {ex.Message}";
            }
        }

        private bool CanDeleteCustomer()
        {
            return SelectedCustomer != null;
        }

        private void DeleteCustomer()
        {
            try
            {
                if (SelectedCustomer == null) return;

                var result = MessageBox.Show(
                    $"Are you sure you want to delete customer '{SelectedCustomer.FullName}'?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _customerService.Delete(SelectedCustomer);
                    LoadCustomers();
                    Message = "Customer deleted successfully!";
                }
            }
            catch (Exception ex)
            {
                Message = $"Error deleting customer: {ex.Message}";
            }
        }

        private void SearchCustomers()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(CustomerSearchKeyword))
                {
                    LoadCustomers();
                }
                else
                {
                    var customers = _customerService.Search(CustomerSearchKeyword);
                    Customers = new ObservableCollection<Customer>(customers);
                }
            }
            catch (Exception ex)
            {
                Message = $"Error searching customers: {ex.Message}";
            }
        }

        private void AddRoom()
        {
            try
            {
                var dialog = new View.RoomDialog();
                var viewModel = new RoomDialogViewModel();
                dialog.DataContext = viewModel;
                dialog.Owner = Application.Current.MainWindow;

                dialog.ShowDialog();
                
                if (viewModel.DialogResult == true)
                {
                    _roomService.Add(viewModel.Room);
                    LoadRooms();
                    Message = "Room added successfully!";
                }
            }
            catch (Exception ex)
            {
                Message = $"Error adding room: {ex.Message}";
            }
        }

        private bool CanUpdateRoom()
        {
            return SelectedRoom != null;
        }

        private void UpdateRoom()
        {
            try
            {
                if (SelectedRoom == null) return;

                var dialog = new View.RoomDialog();
                var viewModel = new RoomDialogViewModel(SelectedRoom);
                dialog.DataContext = viewModel;
                dialog.Owner = Application.Current.MainWindow;

                dialog.ShowDialog();
                
                if (viewModel.DialogResult == true)
                {
                    _roomService.Update(viewModel.Room);
                    LoadRooms();
                    Message = "Room updated successfully!";
                }
            }
            catch (Exception ex)
            {
                Message = $"Error updating room: {ex.Message}";
            }
        }

        private bool CanDeleteRoom()
        {
            return SelectedRoom != null;
        }

        private void DeleteRoom()
        {
            try
            {
                if (SelectedRoom == null) return;

                var result = MessageBox.Show(
                    $"Are you sure you want to delete room '{SelectedRoom.RoomNumber}'?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _roomService.Delete(SelectedRoom);
                    LoadRooms();
                    Message = "Room deleted successfully!";
                }
            }
            catch (Exception ex)
            {
                Message = $"Error deleting room: {ex.Message}";
            }
        }

        private void SearchRooms()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(RoomSearchKeyword))
                {
                    LoadRooms();
                }
                else
                {
                    var rooms = _roomService.Search(RoomSearchKeyword);
                    Rooms = new ObservableCollection<Room>(rooms);
                }
            }
            catch (Exception ex)
            {
                Message = $"Error searching rooms: {ex.Message}";
            }
        }

        private bool CanUpdateBooking()
        {
            return SelectedBooking != null;
        }

        private void UpdateBooking()
        {
            try
            {
                if (!CanUpdateBooking()) return;

                var dialog = new View.BookingDialog();
                var viewModel = new BookingDialogViewModel(SelectedBooking);
                dialog.DataContext = viewModel;
                dialog.Owner = Application.Current.MainWindow;

                dialog.ShowDialog();
                
                if (viewModel.DialogResult == true)
                {
                    _bookingService.Update(viewModel.Booking);
                    Message = $"Booking ID {viewModel.Booking.BookingID} updated successfully.";
                    LoadBookings(); // Refresh the list
                }
            }
            catch (Exception ex)
            {
                Message = $"Error updating booking: {ex.Message}";
            }
        }

        private bool CanDeleteBooking()
        {
            return SelectedBooking != null;
        }

        private void DeleteBooking()
        {
            try
            {
                if (!CanDeleteBooking()) return;

                var result = MessageBox.Show(
                    $"Are you sure you want to delete booking ID '{SelectedBooking.BookingID}'?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.No) return;

                _bookingService.Delete(SelectedBooking);
                Message = $"Booking ID {SelectedBooking.BookingID} deleted successfully.";
                LoadBookings();
            }
            catch (Exception ex)
            {
                Message = $"Error deleting booking: {ex.Message}";
            }
        }

        private void SearchBookings()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(BookingSearchKeyword))
                {
                    LoadBookings();
                }
                else
                {
                    var bookings = _bookingService.Search(BookingSearchKeyword);
                    Bookings = new ObservableCollection<Booking>(bookings);
                }
            }
            catch (Exception ex)
            {
                Message = $"Error searching bookings: {ex.Message}";
            }
        }

        private void GenerateReport()
        {
            try
            {
                var bookings = _bookingService.GetBookingsByDateRange(ReportStartDate, ReportEndDate);
                var sortedBookings = bookings.OrderByDescending(b => b.TotalPrice).ToList();
                
                ReportBookings = new ObservableCollection<Booking>(sortedBookings);
                TotalRevenue = sortedBookings.Sum(b => b.TotalPrice);
                TotalBookings = sortedBookings.Count;

                Message = $"Report generated for period {ReportStartDate:dd/MM/yyyy} to {ReportEndDate:dd/MM/yyyy}";
            }
            catch (Exception ex)
            {
                Message = $"Error generating report: {ex.Message}";
            }
        }

        private bool CanCheckOut()
        {
            return SelectedBooking != null && "Confirmed".Equals(SelectedBooking.Status, StringComparison.OrdinalIgnoreCase);
        }

        private void CheckOut()
        {
            try
            {
                if (!CanCheckOut()) return;

                var result = MessageBox.Show(
                    $"Are you sure you want to check out booking ID '{SelectedBooking.BookingID}'?",
                    "Confirm Check-out",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.No) return;

                // Update booking status
                var bookingToUpdate = _bookingService.GetById(SelectedBooking.BookingID);
                bookingToUpdate.Status = "Completed";
                _bookingService.Update(bookingToUpdate);

                // Update room status
                var roomToUpdate = _roomService.GetById(bookingToUpdate.RoomID);
                if (roomToUpdate != null)
                {
                    roomToUpdate.RoomStatus = "Available";
                    _roomService.Update(roomToUpdate);
                }

                Message = $"Booking ID {bookingToUpdate.BookingID} checked out successfully.";
                RefreshData();
            }
            catch (Exception ex)
            {
                Message = $"Error during check-out: {ex.Message}";
            }
        }

        private void RefreshData()
        {
            LoadData();
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
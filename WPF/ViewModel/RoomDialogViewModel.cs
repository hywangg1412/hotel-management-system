using Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WPF.Commands;
using Bussiness;

namespace WPF.ViewModel
{
    public class RoomDialogViewModel : INotifyPropertyChanged
    {
        private readonly RoomTypeService _roomTypeService;
        private Room _room;
        private string _dialogTitle;
        private bool _isEditMode;
        private RoomType _selectedRoomType;
        private ObservableCollection<RoomType> _roomTypes;
        private bool? _dialogResult;

        public RoomDialogViewModel(Room room = null)
        {
            _roomTypeService = new RoomTypeService();
            _room = room ?? new Room();
            _isEditMode = room != null;
            _dialogTitle = _isEditMode ? "Edit Room" : "Add New Room";

            LoadRoomTypes();

            SaveCommand = new RelayCommand(_ => Save());
            CancelCommand = new RelayCommand(_ => Cancel());
        }

        public Room Room
        {
            get => _room;
            set
            {
                _room = value;
                OnPropertyChanged(nameof(Room));
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

        public RoomType SelectedRoomType
        {
            get => _selectedRoomType;
            set
            {
                _selectedRoomType = value;
                if (value != null)
                {
                    Room.RoomTypeId = value.RoomTypeId;
                }
                OnPropertyChanged(nameof(SelectedRoomType));
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

        private void LoadRoomTypes()
        {
            try
            {
                var roomTypes = _roomTypeService.GetAll();
                RoomTypes = new ObservableCollection<RoomType>(roomTypes);
                
                if (IsEditMode && Room.RoomTypeId > 0)
                {
                    SelectedRoomType = RoomTypes.FirstOrDefault(rt => rt.RoomTypeId == Room.RoomTypeId);
                }
                else if (RoomTypes.Count > 0)
                {
                    SelectedRoomType = RoomTypes.First();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading room types: {ex.Message}", "Error", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Save()
        {
            if (string.IsNullOrWhiteSpace(Room.RoomNumber) || 
                string.IsNullOrWhiteSpace(Room.RoomDescription) ||
                Room.RoomMaxCapacity <= 0 ||
                Room.RoomPricePerDate <= 0)
            {
                MessageBox.Show("Please fill in all required fields with valid values.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (SelectedRoomType == null)
            {
                MessageBox.Show("Please select a room type.", "Validation Error", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Set default values if not provided
            if (Room.RoomId == 0)
            {
                Room.RoomId = new Random().Next(1000, 9999);
            }
            if (string.IsNullOrWhiteSpace(Room.RoomStatus))
            {
                Room.RoomStatus = "Available";
            }

            Room.RoomTypeId = SelectedRoomType.RoomTypeId;

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
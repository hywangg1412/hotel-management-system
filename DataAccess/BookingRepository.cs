using DataAccess.Context.Common;
using DataAccess.Interfaces;
using Models;

namespace DataAccess
{
    public class BookingRepository : IBookingRepository
    {
        private static List<Booking> _bookingList;

        public BookingRepository()
        {
            if (_bookingList == null)
            {
                _bookingList = new List<Booking>();
                
                // Add sample data
                var sampleBookings = new List<Booking>
                {
                    new Booking
                    {
                        BookingID = 1,
                        CustomerID = 1,
                        RoomID = 1,
                        StartDate = DateTime.Today.AddDays(-10),
                        EndDate = DateTime.Today.AddDays(-7),
                        TotalPrice = 300.00m,
                        Status = "Completed"
                    },
                    new Booking
                    {
                        BookingID = 2,
                        CustomerID = 1,
                        RoomID = 2,
                        StartDate = DateTime.Today.AddDays(-5),
                        EndDate = DateTime.Today.AddDays(-2),
                        TotalPrice = 400.00m,
                        Status = "Completed"
                    },
                    new Booking
                    {
                        BookingID = 3,
                        CustomerID = 1,
                        RoomID = 1,
                        StartDate = DateTime.Today.AddDays(5),
                        EndDate = DateTime.Today.AddDays(8),
                        TotalPrice = 300.00m,
                        Status = "Confirmed"
                    }
                };
                
                _bookingList.AddRange(sampleBookings);
            }
        }

        public void Add(Booking entity)
        {
            try
            {
                var isRoomAvailable = !_bookingList.Any(b =>
                    b.RoomID == entity.RoomID &&
                    b.Status != "Cancelled" &&
                    ((entity.StartDate >= b.StartDate && entity.StartDate < b.EndDate) ||
                     (entity.EndDate > b.StartDate && entity.EndDate <= b.EndDate)));

                if (!isRoomAvailable)
                {
                    AppLogger.LogWarning($"Room {entity.RoomID} is not available for the selected dates");
                    throw new InvalidOperationException("Room is not available for the selected dates");
                }

                _bookingList.Add(entity);
                AppLogger.LogInformation($"Booking added for Room {entity.RoomID} from {entity.StartDate} to {entity.EndDate}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error adding booking for Room {entity.RoomID}");
                throw;
            }
        }

        public void Delete(Booking entity)
        {
            try
            {
                _bookingList.Remove(entity);
                AppLogger.LogInformation($"Booking deleted for Room {entity.RoomID}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error deleting booking for Room {entity.RoomID}");
                throw;
            }
        }

        public List<Booking> GetAll()
        {
            try
            {
                AppLogger.LogInformation("Getting all bookings");
                return _bookingList;
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, "Error getting all bookings");
                throw;
            }
        }

        public Booking GetById(int id)
        {
            try
            {
                AppLogger.LogInformation($"Getting booking by ID: {id}");
                var booking = _bookingList.FirstOrDefault(b => b.BookingID == id);
                if (booking == null)
                {
                    AppLogger.LogWarning($"Booking not found with ID: {id}");
                }
                return booking;
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error getting booking by ID: {id}");
                throw;
            }
        }

        public List<Booking> Search(string keyword)
        {
            try
            {
                AppLogger.LogInformation($"Searching bookings with keyword: {keyword}");
                var result = _bookingList
                    .Where(b => b.BookingID.ToString().Contains(keyword) ||
                               b.RoomID.ToString().Contains(keyword) ||
                               b.CustomerID.ToString().Contains(keyword) ||
                               b.Status.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                AppLogger.LogInformation($"Found {result.Count} bookings matching keyword: {keyword}");
                return result;
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error searching bookings with keyword: {keyword}");
                throw;
            }
        }

        public void Update(Booking entity)
        {
            try
            {
                var existingBooking = GetById(entity.BookingID);
                if (existingBooking == null)
                {
                    AppLogger.LogWarning($"Booking not found for update. ID: {entity.BookingID}");
                    throw new KeyNotFoundException($"Booking with ID {entity.BookingID} not found");
                }

                var isRoomAvailable = !_bookingList.Any(b =>
                    b.BookingID != entity.BookingID &&
                    b.RoomID == entity.RoomID &&
                    b.Status != "Cancelled" &&
                    ((entity.StartDate >= b.StartDate && entity.StartDate < b.EndDate) ||
                     (entity.EndDate > b.StartDate && entity.EndDate <= b.EndDate)));

                if (!isRoomAvailable)
                {
                    AppLogger.LogWarning($"Room {entity.RoomID} is not available for the selected dates");
                    throw new InvalidOperationException("Room is not available for the selected dates");
                }

                existingBooking.CustomerID = entity.CustomerID;
                existingBooking.RoomID = entity.RoomID;
                existingBooking.StartDate = entity.StartDate;
                existingBooking.EndDate = entity.EndDate;
                existingBooking.TotalPrice = entity.TotalPrice;
                existingBooking.Status = entity.Status;

                AppLogger.LogInformation($"Booking updated: ID {entity.BookingID}");
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error updating booking: ID {entity.BookingID}");
                throw;
            }
        }

        public List<Booking> GetBookingsByCustomer(int customerId)
        {
            try
            {
                AppLogger.LogInformation($"Getting bookings for customer: {customerId}");
                return _bookingList.Where(b => b.CustomerID == customerId).ToList();
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error getting bookings for customer: {customerId}");
                throw;
            }
        }

        public List<Booking> GetBookingsByRoom(int roomId)
        {
            try
            {
                AppLogger.LogInformation($"Getting bookings for room: {roomId}");
                return _bookingList.Where(b => b.RoomID == roomId).ToList();
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error getting bookings for room: {roomId}");
                throw;
            }
        }

        public List<Booking> GetBookingsByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                AppLogger.LogInformation($"Getting bookings from {startDate} to {endDate}");
                return _bookingList
                    .Where(b => b.StartDate >= startDate && b.EndDate <= endDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error getting bookings by date range");
                throw;
            }
        }
    }
}

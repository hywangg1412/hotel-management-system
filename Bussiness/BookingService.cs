using Bussiness.Interfaces;
using DataAccess;
using DataAccess.Context.Common;
using Models;

namespace Bussiness
{
    public class BookingService : IBookingService
    {
        private readonly BookingRepository _bookingRepository;

        public BookingService()
        {
            _bookingRepository = new BookingRepository();
        }

        public void Add(Booking entity)
        {
            try
            {
                AppLogger.LogInformation($"Adding booking for Room {entity.RoomID} from {entity.StartDate} to {entity.EndDate}");
                _bookingRepository.Add(entity);
                AppLogger.LogInformation($"Booking added successfully for Room {entity.RoomID}");
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
                AppLogger.LogInformation($"Deleting booking for Room {entity.RoomID}");
                _bookingRepository.Delete(entity);
                AppLogger.LogInformation($"Booking deleted successfully for Room {entity.RoomID}");
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
                var result = _bookingRepository.GetAll();
                AppLogger.LogInformation($"Found {result.Count} bookings");
                return result;
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
                var booking = _bookingRepository.GetById(id);
                if (booking == null)
                {
                    AppLogger.LogWarning($"Booking not found with ID: {id}");
                }
                else
                {
                    AppLogger.LogInformation($"Booking found with ID: {id}");
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
                var result = _bookingRepository.Search(keyword);
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
                AppLogger.LogInformation($"Updating booking ID {entity.BookingID}");
                _bookingRepository.Update(entity);
                AppLogger.LogInformation($"Booking updated successfully: ID {entity.BookingID}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error updating booking: ID {entity.BookingID}");
                throw;
            }
        }

        public List<Booking> GetBookingsByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                AppLogger.LogInformation($"Getting bookings from {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}");
                var result = _bookingRepository.GetBookingsByDateRange(startDate, endDate);
                AppLogger.LogInformation($"Found {result.Count} bookings in date range");
                return result;
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error getting bookings by date range: {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}");
                throw;
            }
        }

        public List<Booking> GetBookingsByCustomer(int customerId)
        {
            try
            {
                AppLogger.LogInformation($"Getting bookings for customer ID: {customerId}");
                var result = _bookingRepository.GetBookingsByCustomer(customerId);
                AppLogger.LogInformation($"Found {result.Count} bookings for customer ID: {customerId}");
                return result;
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error getting bookings for customer ID: {customerId}");
                throw;
            }
        }
    }
}

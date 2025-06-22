using Models;

namespace DataAccess.Interfaces
{
    public interface IBookingRepository : Repository<Booking, int>
    {
        List<Booking> GetBookingsByDateRange(DateTime startDate, DateTime endDate);
        List<Booking> GetBookingsByCustomer(int customerId);
        List<Booking> GetBookingsByRoom(int roomId);
    }
}

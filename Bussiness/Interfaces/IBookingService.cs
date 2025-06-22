using Models;

namespace Bussiness.Interfaces
{
    public interface IBookingService : Service<Booking, int>
    {
        List<Booking> GetBookingsByDateRange(DateTime startDate, DateTime endDate);
        List<Booking> GetBookingsByCustomer(int customerId);
    }
}

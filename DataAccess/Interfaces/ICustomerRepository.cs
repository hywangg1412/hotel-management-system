using Models;

namespace DataAccess.Interfaces
{
    public interface ICustomerRepository : Repository<Customer, int>
    {
        Customer Login(string email, string password);
    }
}

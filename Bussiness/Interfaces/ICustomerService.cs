using Models;

namespace Bussiness.Interfaces
{
    public interface ICustomerService : Service<Customer, int>
    {
        Customer Login(string email, string password);
    }
}

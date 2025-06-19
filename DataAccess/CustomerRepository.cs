using DataAccess.Context.Common;
using DataAccess.Interfaces;
using Models;

namespace DataAccess
{
    public class CustomerRepository : ICustomerRepository
    {
        private static List<Customer> _customerList;

        public CustomerRepository()
        {
            _customerList = new List<Customer>();
        }
        public void Add(Customer entity)
        {
            try
            {
                _customerList.Add(entity);
                AppLogger.LogInformation($"Customer added: {entity.FullName}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error adding customer: {entity.FullName}");
                throw;
            }
        }

        public void Delete(Customer entity)
        {
            try
            {
                _customerList.Remove(entity);
                AppLogger.LogInformation($"Delete Customer: {entity.FullName}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error deleting customer: {entity.FullName}");
                throw;
            }
        }

        public List<Customer> GetAll()
        {
            try
            {
                AppLogger.LogInformation("Getting all customers");
                return _customerList;
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, "Error getting all customers");
                throw;
            }
        }

        public Customer GetById(int id)
        {
            try
            {
                AppLogger.LogInformation($"Getting customer by ID: {id}");
                var customer = _customerList.FirstOrDefault(c => c.UserID == id);
                if (customer == null)
                {
                    AppLogger.LogWarning($"Customer not found with ID: {id}");
                }
                return customer;
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error getting customer by ID: {id}");
                throw;
            }
        }

        public List<Customer> Search(string keyword)
        {
            try
            {
                AppLogger.LogInformation($"Searching customers with keyword: {keyword}");
                var result = _customerList
                    .Where(c => c.FullName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                               c.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                AppLogger.LogInformation($"Found {result.Count} customers matching keyword: {keyword}");
                return result;
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error searching customers with keyword: {keyword}");
                throw;
            }
        }

        public void Update(Customer entity)
        {
            try
            {
                var existingCustomer = GetById(entity.UserID);
                if (existingCustomer == null)
                {
                    AppLogger.LogWarning($"Customer not found for update. ID: {entity.UserID}");
                    throw new KeyNotFoundException($"Customer with ID {entity.UserID} not found");
                }

                existingCustomer.FullName = entity.FullName;
                existingCustomer.Email = entity.Email;
                existingCustomer.Birthday = entity.Birthday;
                existingCustomer.Status = entity.Status;
                existingCustomer.Password = entity.Password;
                existingCustomer.Telephone = entity.Telephone;

                AppLogger.LogInformation($"Customer updated: {entity.FullName}");
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error updating customer: {entity.FullName}");
                throw;
            }
        }

        public Customer Login(string email, string password)
        {
            return _customerList.FirstOrDefault(c => c.Email == email && c.Password == password);
        }
    }
}

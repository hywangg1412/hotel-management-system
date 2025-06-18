using Bussiness.Interfaces;
using Models;
using DataAccess;
using DataAccess.Interfaces;
using DataAccess.Context.Common;

namespace Bussiness
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService()
        {
            _customerRepository = new CustomerRepository();
        }

        public void Add(Customer entity)
        {
            try
            {
                AppLogger.LogInformation($"Adding customer: {entity.FullName}");
                _customerRepository.Add(entity);
                AppLogger.LogInformation($"Customer added successfully: {entity.FullName}");
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
                AppLogger.LogInformation($"Deleting customer: {entity.FullName}");
                _customerRepository.Delete(entity);
                AppLogger.LogInformation($"Customer deleted successfully: {entity.FullName}");
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
                var result = _customerRepository.GetAll();
                AppLogger.LogInformation($"Found {result.Count} customers");
                return result;
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
                var customer = _customerRepository.GetById(id);
                if (customer == null)
                {
                    AppLogger.LogWarning($"Customer not found with ID: {id}");
                }
                else
                {
                    AppLogger.LogInformation($"Customer found with ID: {id}");
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
                var result = _customerRepository.Search(keyword);
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
                AppLogger.LogInformation($"Updating customer: {entity.FullName}");
                _customerRepository.Update(entity);
                AppLogger.LogInformation($"Customer updated successfully: {entity.FullName}");
            }
            catch (Exception ex)
            {
                AppLogger.LogError(ex, $"Error updating customer: {entity.FullName}");
                throw;
            }
        }
    }
}

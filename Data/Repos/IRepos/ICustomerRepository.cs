using RestaurantProject.Models;

namespace RestaurantProject.Data.Repos.IRepos
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Customer customer);
        Task<Customer> FindCustomerByIdAsync(int customerId);
        Task<Customer> FindCustomerByPhoneNoAsync(string phoneNo);

    }
}

using RestaurantProject.Models;
using RestaurantProject.Models.DTOs;

namespace RestaurantProject.Services.IServices
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync();
        Task AddCustomerAsync(CustomerDTO customer);
        Task DeleteCustomerAsync(int customerId);
        Task UpdateCustomerAsync(int customerId, CustomerDTO customer);
        Task<CustomerDTO> FindCustomerByIdAsync(int customerId);
        Task<Customer> FindCustomerByPhoneNoAsync(string phoneNo);

    }
}

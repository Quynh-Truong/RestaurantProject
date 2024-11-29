using RestaurantProject.Models;
using RestaurantProject.Models.DTOs;

namespace RestaurantProject.Services.IServices
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerShowDTO>> GetAllCustomersAsync();
        Task AddCustomerAsync(CustomerCreateDTO customer);
        Task DeleteCustomerAsync(int customerId);
        Task UpdateCustomerAsync(int customerId, CustomerCreateDTO customer);
        Task<CustomerCreateDTO> FindCustomerByIdAsync(int customerId);
        Task<Customer> FindCustomerByPhoneNoAsync(string phoneNo);

    }
}

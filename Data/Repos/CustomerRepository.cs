using Microsoft.EntityFrameworkCore;
using RestaurantProject.Data.Repos.IRepos;
using RestaurantProject.Models;

namespace RestaurantProject.Data.Repos
{
    public class CustomerRepository : ICustomerRepository//gets context dep. injection
    {
        private readonly RestaurantContext _context;

        public CustomerRepository(RestaurantContext context)
        {
            _context = context;
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(Customer customer)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }


        public async Task<Customer> FindCustomerByIdAsync(int customerId)
        {
            return await _context.Customers.SingleOrDefaultAsync(c => c.CustomerId == customerId);
        }

        public async Task<Customer> FindCustomerByPhoneNoAsync(string phoneNo)
        {
            return await _context.Customers.SingleOrDefaultAsync(c => c.PhoneNo == phoneNo);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }
    }
}

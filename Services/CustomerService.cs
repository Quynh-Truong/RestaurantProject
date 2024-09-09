using RestaurantProject.Data.Repos;
using RestaurantProject.Data.Repos.IRepos;
using RestaurantProject.Exceptions;
using RestaurantProject.Models;
using RestaurantProject.Models.DTOs;
using RestaurantProject.Services.IServices;

namespace RestaurantProject.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;//gets repo dep injection

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }


        public async Task AddCustomerAsync(CustomerDTO customer)
        {
            var existingCustomer = await _customerRepository.FindCustomerByPhoneNoAsync(customer.PhoneNo);

            if (existingCustomer != null)
            {
                throw new ValidationException("This phone number already exists!");
            }

            var newCustomer = new Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNo = customer.PhoneNo
            };

            await _customerRepository.AddCustomerAsync(newCustomer);

        }

        public async Task DeleteCustomerAsync(int customerId)
        {
            var customer = await _customerRepository.FindCustomerByIdAsync(customerId);

            if (customer == null)
            {
                throw new NotFoundException($"Customer with ID {customerId} not found.");
            }
            await _customerRepository.DeleteCustomerAsync(customer);
        }

        public async Task<CustomerDTO> FindCustomerByIdAsync(int customerId)
        {
            var customerChosen = await _customerRepository.FindCustomerByIdAsync(customerId);

            if (customerChosen != null)
            {
                var customer = new CustomerDTO
                {
                    FirstName = customerChosen.FirstName,
                    LastName = customerChosen.LastName,
                    PhoneNo = customerChosen.PhoneNo
                };
                return customer;
            }
            return null;
        }

        public async Task<Customer> FindCustomerByPhoneNoAsync(string phoneNo)
        {
            var existingCustomer = await _customerRepository.FindCustomerByPhoneNoAsync(phoneNo);
            return existingCustomer;
        }

        public async Task<IEnumerable<CustomerDTO>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();

            var customerList = customers.Select(c => new CustomerDTO
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNo = c.PhoneNo
            }).ToList();

            return customerList;
        }

        public async Task UpdateCustomerAsync(int customerId, CustomerDTO customer)
        {
            var chosenCustomer = await _customerRepository.FindCustomerByIdAsync(customerId);
            if (chosenCustomer == null)
            {
                throw new NotFoundException($"Customer with ID {customerId} not found.");
            }

            //if the customer already exists, check that it is NOT the customer being updated atm
            var existingCustomer = await _customerRepository.FindCustomerByPhoneNoAsync(customer.PhoneNo);
            if (existingCustomer != null && existingCustomer.CustomerId != customerId)
            {
                throw new ValidationException("This phone number already exists!");
            }

            //re-writing old values
            chosenCustomer.FirstName = customer.FirstName;
            chosenCustomer.LastName = customer.LastName;
            chosenCustomer.PhoneNo = customer.PhoneNo;

            await _customerRepository.UpdateCustomerAsync(chosenCustomer);
        }

    }
}

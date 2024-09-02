using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantProject.Exceptions;
using RestaurantProject.Models.DTOs;
using RestaurantProject.Services.IServices;
using System.ComponentModel.DataAnnotations;
using ValidationException = RestaurantProject.Exceptions.ValidationException;

namespace RestaurantProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;//service dep inj

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("getAllCustomers")]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpPost("addCustomer")]
        public async Task<ActionResult> AddCustomer(CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)//check that all required fields are there
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _customerService.AddCustomerAsync(customerDTO);
                return Created();
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in handling request");
            }
        }

        [HttpGet("getCustomer/{customerId}")]
        public async Task<ActionResult> FindCustomerById(int customerId)
        {
            var customer = await _customerService.FindCustomerByIdAsync(customerId);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpGet("getCustomerByPhoneNo/{phoneNo}")]
        public async Task<ActionResult> FindCustomerByPhoneNo(string phoneNo)
        {
            var customer = await _customerService.FindCustomerByPhoneNoAsync(phoneNo);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPut("updateCustomer/{customerId}")]
        public async Task<ActionResult> UpdateCustomer(int customerId, CustomerDTO customerDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _customerService.UpdateCustomerAsync(customerId, customerDTO);
                return NoContent();
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception)
            {
                return StatusCode(500, "Error in handling request");
            }

        }

        [HttpDelete("deleteCustomer/{customerId}")]
        public async Task <ActionResult> DeleteCustomer(int customerId)
        {
            try
            {
                await _customerService.DeleteCustomerAsync(customerId);
                return Ok();
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error in handling request");
            }

        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineStore.Models;
using OnlineStore.Services;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<JsonResult> GetCustomers()
        {
            var customers = await _customerService.GetAllAsync();
            return new JsonResult(customers);
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetCustomer(Guid id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
            {
                return  new JsonResult(NotFound()); 
            }
            return new JsonResult(customer);
        }

        [HttpPost]
        public async Task<JsonResult> PostCustomer(Customer customer)
        {
            await _customerService.AddAsync(customer);
            return new JsonResult(CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer));
        }

        [HttpPut("{id}")]
        public async Task<JsonResult> PutCustomer(Guid id, Customer customer)
        {
            if (id != customer.Id)
            {
                return new JsonResult(BadRequest());
            }
            await _customerService.UpdateAsync(customer);
            return new JsonResult(NoContent());
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteCustomer(Guid id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
            {
                return new JsonResult(NotFound());
            }
            await _customerService.DeleteAsync(id);
            return new JsonResult(NoContent());
        }
    }
}

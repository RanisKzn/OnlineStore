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
        private readonly IGenericService<Customer> _genericService;
        private readonly IGenericService<User> _userService;

        public CustomersController(IGenericService<Customer> genericService, IGenericService<User> userService)
        {
            _genericService = genericService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<JsonResult> GetCustomers()
        {
            var customers = await _genericService.GetAllAsync();
            return new JsonResult(customers);
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetCustomer(Guid id)
        {
            var customer = await _genericService.GetByIdAsync(id);
            if (customer == null)
            {
                return  new JsonResult(NotFound()); 
            }
            return new JsonResult(customer);
        }

        [HttpPost]
        public async Task<JsonResult> PostCustomer([FromBody]Customer customer)
        {
            if (customer == null)
            {
                return new JsonResult(BadRequest("Invalid customer data"));
            }
            customer.User = await _userService.GetByIdAsync(customer.UserId);
            await _genericService.AddAsync(customer);
            return new JsonResult(CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer));
        }

        [HttpPut("{id}")]
        public async Task<JsonResult> PutCustomer(Guid id, Customer customer)
        {
            if (id != customer.Id)
            {
                return new JsonResult(BadRequest());
            }
            await _genericService.UpdateAsync(customer);
            return new JsonResult(NoContent());
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteCustomer(Guid id)
        {
            var customer = await _genericService.GetByIdAsync(id);
            if (customer == null)
            {
                return new JsonResult(NotFound());
            }
            await _genericService.DeleteAsync(id);
            return new JsonResult(NoContent());
        }
    }
}

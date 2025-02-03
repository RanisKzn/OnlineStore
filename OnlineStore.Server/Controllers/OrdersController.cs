using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using OnlineStore.Services;

namespace OnlineStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IGenericService<Order> _genericService;

        public OrdersController(IGenericService<Order> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<JsonResult> GetOrders()
        {
            var orders = await _genericService.GetAllAsync();
            return new JsonResult(orders);
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetOrder(Guid id)
        {
            var order = await _genericService.GetByIdAsync(id);
            return new JsonResult(order);
        }

        [HttpPost]
        public async Task<JsonResult> PostOrder([FromBody] Order order)
        {
            var orders = await _genericService.GetAllAsync();
            if (order != null)
            {
                order.OrderNumber  = orders.OrderByDescending(o => o.OrderNumber).FirstOrDefault().OrderNumber + 1;
            }
            await _genericService.AddAsync(order);
            return new JsonResult(CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order));
        }

        [HttpPut("{id}")]
        public async Task<JsonResult> PutOrder(Guid id,[FromBody]Order order)
        {
            if (id != order.Id)
            {
                return new JsonResult(BadRequest());
            }
            await _genericService.UpdateAsync(order);
            return new JsonResult(NoContent());
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteOrder(Guid id)
        {
            var order = await _genericService.GetByIdAsync(id);
            if (id != order.Id)
            {
                return new JsonResult(BadRequest());
            }
            await _genericService.DeleteAsync(id);
            return new JsonResult(NoContent());
        }
    }
}

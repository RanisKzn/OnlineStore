using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using OnlineStore.Services;

namespace OnlineStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {
        private readonly IGenericService<OrderItem> _genericService;

        public OrderItemsController(IGenericService<OrderItem> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<JsonResult> GetOrderItems()
        {
            var orderItems = await _genericService.GetAllAsync();
            return new JsonResult(orderItems);
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetOrderItem(Guid id)
        {
            var orderItems = await _genericService.GetByIdAsync(id);
            return new JsonResult(orderItems);
        }

        [HttpPost]
        public async Task<JsonResult> PostOrderItem(OrderItem orderItem)
        {
            await _genericService.AddAsync(orderItem);
            return new JsonResult(CreatedAtAction(nameof(GetOrderItem), new { id = orderItem.Id }, orderItem));
        }

        [HttpPut("{id}")]
        public async Task<JsonResult> PutOrderItem(Guid id, OrderItem orderItem)
        {
            if (id != orderItem.Id)
            {
                return new JsonResult(BadRequest());
            }
            await _genericService.UpdateAsync(orderItem);
            return new JsonResult(NoContent());
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteOrderItem(Guid id)
        {
            var orderItem = await _genericService.GetByIdAsync(id);
            if (id != orderItem.Id)
            {
                return new JsonResult(BadRequest());
            }
            await _genericService.DeleteAsync(id);
            return new JsonResult(NoContent());
        }
    }
}

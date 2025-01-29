using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using OnlineStore.Services;

namespace OnlineStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IGenericService<Item> _genericService;

        public ItemsController(IGenericService<Item> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<JsonResult> GetItems()
        {
            var items = await _genericService.GetAllAsync();
            return new JsonResult(items);
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetItem(Guid id)
        {
            var item = await _genericService.GetByIdAsync(id);
            return new JsonResult(item);
        }

        [HttpPost]
        public async Task<JsonResult> PostItem(Item item)
        {
            await _genericService.AddAsync(item);
            return new JsonResult(CreatedAtAction(nameof(GetItem), new { id = item.Id }, item));
        }

        [HttpPut("{id}")]
        public async Task<JsonResult> PutItem(Guid id, Item item)
        {
            if (id != item.Id)
            {
                return new JsonResult(BadRequest());
            }
            await _genericService.UpdateAsync(item);
            return new JsonResult(NoContent());
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteItem(Guid id)
        {
            var item = await _genericService.GetByIdAsync(id);
            if (id != item.Id)
            {
                return new JsonResult(BadRequest());
            }
            await _genericService.DeleteAsync(id);
            return new JsonResult(NoContent());
        }
    }
}

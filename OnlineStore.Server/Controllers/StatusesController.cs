using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using OnlineStore.Services;

namespace OnlineStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly IGenericService<Status> _genericService;

        public StatusesController(IGenericService<Status> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<JsonResult> GetStatuses()
        {
            var statuses = await _genericService.GetAllAsync();
            return new JsonResult(statuses);
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetStatus(Guid id)
        {
            var status = await _genericService.GetByIdAsync(id);
            return new JsonResult(status);
        }

        [HttpPost]
        public async Task<JsonResult> PostStatus(Status status)
        {
            await _genericService.AddAsync(status);
            return new JsonResult(CreatedAtAction(nameof(GetStatus), new { id = status.Id }, status));
        }

        [HttpPut("{id}")]
        public async Task<JsonResult> PutStatus(Guid id, Status status)
        {
            if (id != status.Id)
            {
                return new JsonResult(BadRequest());
            }
            await _genericService.UpdateAsync(status);
            return new JsonResult(NoContent());
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteStatus(Guid id)
        {
            var status = await _genericService.GetByIdAsync(id);
            if (id != status.Id)
            {
                return new JsonResult(BadRequest());
            }
            await _genericService.DeleteAsync(id);
            return new JsonResult(NoContent());
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using OnlineStore.Services;

namespace OnlineStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IGenericService<Role> _genericService;

        public RolesController(IGenericService<Role> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<JsonResult> GetRoles()
        {
            var roles = await _genericService.GetAllAsync();
            return new JsonResult(roles);
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetRole(Guid id)
        {
            var role = await _genericService.GetByIdAsync(id);
            return new JsonResult(role);
        }

        [HttpPost]
        public async Task<JsonResult> PostRole(Role role)
        {
            await _genericService.AddAsync(role);
            return new JsonResult(CreatedAtAction(nameof(GetRole), new { id = role.Id }, role));
        }

        [HttpPut("{id}")]
        public async Task<JsonResult> PutRole(Guid id, Role role)
        {
            if (id != role.Id)
            {
                return new JsonResult(BadRequest());
            }
            await _genericService.UpdateAsync(role);
            return new JsonResult(NoContent());
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteRole(Guid id)
        {
            var role = await _genericService.GetByIdAsync(id);
            if (id != role.Id)
            {
                return new JsonResult(BadRequest());
            }
            await _genericService.DeleteAsync(id);
            return new JsonResult(NoContent());
        }
    }
}

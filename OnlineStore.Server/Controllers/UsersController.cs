using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using OnlineStore.Services;

namespace OnlineStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IGenericService<User> _genericService;
        private readonly IGenericService<Role> _roleService;

        public UsersController(IGenericService<User> genericService, IGenericService<Role> roleService)
        {
            _genericService = genericService;
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<JsonResult> GetUsers()
        {
            var users = await _genericService.GetAllAsync();
            return new JsonResult(users);
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetUser(Guid id)
        {
            var user = await _genericService.GetByIdAsync(id);
            return new JsonResult(user);
        }

        [HttpPost]
        public async Task<JsonResult> PostUser([FromBody]User user)
        {
            if (user == null)
            {
                return new JsonResult(BadRequest("Invalid customer data"));
            }
            user.Role = await _roleService.GetByIdAsync(user.RoleId);
            await _genericService.AddAsync(user);
            return new JsonResult(CreatedAtAction(nameof(GetUser), new { id = user.Id }, user));
        }

        [HttpPut("{id}")]
        public async Task<JsonResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return new JsonResult(BadRequest());
            }
            await _genericService.UpdateAsync(user);
            return new JsonResult(NoContent());
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteUser(Guid id)
        {
            var user = await _genericService.GetByIdAsync(id);
            if (id != user.Id)
            {
                return new JsonResult(BadRequest());
            }
            await _genericService.DeleteAsync(id);
            return new JsonResult(NoContent());
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Models;
using OnlineStore.Server.Services;
using OnlineStore.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IGenericService<Role> _roleService;
        private readonly IGenericService<Customer> _customerService;

        public AuthController(IConfiguration configuration, IUserService userService, IGenericService<Role> roleService, IGenericService<Customer> customerService)
        {
            _configuration = configuration;
            _userService = userService;
            _roleService = roleService;
            _customerService = customerService;
        }

        [HttpPost("login")]
        public async Task<JsonResult> Login([FromBody] LoginModel model)
        {
            if (model == null )
            {
                return new JsonResult(BadRequest("Invalid client request"));
            }

            var user = await _userService.GetByUsernameAsync(model.Username);
            if (user == null || user.Password != model.Password)
            {
                return new JsonResult(Unauthorized());
            }

            var userRole = await _roleService.GetByIdAsync(user.RoleId);
            var users = await _customerService.GetAllAsync();
            var tokenString = GenerateJwtToken(user, userRole.Name);
            if (userRole.Name == "Customer" )
            {
                var customer = users.FirstOrDefault(u => u.UserId == user.Id);
                return new JsonResult(Ok(new { token = tokenString, role = userRole.Name, customer = customer }));
            }
            return new JsonResult(Ok(new { token = tokenString, role=userRole.Name, name= model.Username }));
        }

        private string GenerateJwtToken(User user, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, role) 
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return  new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.Models;
using OnlineStore.Server.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace OnlineStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<JsonResult> Login([FromBody] User userLogin)
        {
            if (userLogin == null)
            {
                return new JsonResult(BadRequest("Invalid client request"));
            }

            var user = await _userService.GetByUsernameAsync(userLogin.Username);
            if (user == null || user.Password != userLogin.Password)
            {
                return new JsonResult(Unauthorized());
            }

            var tokenString = GenerateJSONWebToken(user);
            return new JsonResult(Ok(new { token = tokenString }));
        }

        private async Task<JsonResult> GenerateJSONWebToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:Token"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["AppSettings:Issuer"],
              _configuration["AppSettings:Audience"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JsonResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}

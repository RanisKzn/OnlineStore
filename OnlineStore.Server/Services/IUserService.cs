using OnlineStore.Models;

namespace OnlineStore.Server.Services
{
    public interface IUserService
    {
        Task<User> GetByUsernameAsync(string username);
    }
}

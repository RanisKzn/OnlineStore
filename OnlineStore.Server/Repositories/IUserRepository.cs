using OnlineStore.Models;

namespace OnlineStore.Server.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
    }
}

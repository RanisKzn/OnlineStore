using OnlineStore.Data;
using OnlineStore.Models;

namespace OnlineStore.Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly OnlineStoreDbContext _context;

        public UserRepository(OnlineStoreDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.FindAsync(username);
        }
    }
}

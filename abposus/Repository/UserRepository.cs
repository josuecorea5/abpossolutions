using abposus.Data;
using abposus.Interfaces;
using abposus.Models;
using Microsoft.EntityFrameworkCore;

namespace abposus.Repository
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppUser>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}

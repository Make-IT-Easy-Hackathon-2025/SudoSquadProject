using Microsoft.EntityFrameworkCore;
using RanklUpp.Infrastructure.Context;
using RankUpp.Application.Interfaces.Repositories;
using RankUpp.Core.Models;

namespace RanklUpp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly RankUppDbContext _context;

        public UserRepository(RankUppDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User user, CancellationToken cancellationToken = default)
        {
            var result = await _context.AddAsync(user);

            await _context.SaveChangesAsync(cancellationToken);

            return result.Entity;
        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public async Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> IsEmailUsedAsync(string email, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if(user == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> IsUserNameUsedAsync(string userName, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
            {
                return false;
            }

            return true;
        }
    }
}

using Domain.Entities.DTOs;
using Domain.Entities.Models;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        protected readonly AppDBContext _context;

        public UserRepository(AppDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CheckPassword(int userId, string password)
        {
            return await _context.User.Where(x => x.Id == userId && x.Password == password).AnyAsync();
        }

        public async Task<IEnumerable<VerifiedUserDTO>> GetVerifiedUsers()
        {
            return await _context.User.Where(x => x.IsVerified == true).Select(x => new VerifiedUserDTO { Email = x.Email, Roles = x.Roles, UserName = x.UserName }).ToListAsync();
        }

        public async Task UpdatePassword(int userId, string newPassword)
        {
            var user = await _context.User.FirstOrDefaultAsync(user => user.Id == userId);
            if (user != null)
            {
                user.Password = newPassword;
                _context.Attach(user);
                _context.Entry(user).Property(x => x.Password).IsModified = true;
            }
        }
    }
}

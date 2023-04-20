using Microsoft.EntityFrameworkCore;
using PMS.Core.Entities;
using PMS.Core.Repository;
using PMS.Infrastructure.Data.Context;
using PMS.Infrastructure.Exceptions;

namespace PMS.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly PmsDbContext _context;



        public UserRepository(PmsDbContext context)
        {
            _context = context;
        }



        public async Task<User> GetAsync(Guid id)
            => await Task.FromResult(await _context.Users.SingleOrDefaultAsync(u => u.Id == id)
                ?? throw new UserNotFoundException($"User with ID '{id}' DOES NOT EXIST"));

        public async Task<User> GetAsync(string email)
            => await Task.FromResult(await _context.Users.SingleOrDefaultAsync(u => u.Email.ToLower() == email.ToLower())
                ?? throw new UserNotFoundException($"User with Email '{email}' does not exist"));

        public async Task CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
             _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using PMS.Core.Entities;
using PMS.Core.Repository;
using PMS.Infrastructure.Data.Context;
using PMS.Infrastructure.Exceptions;

namespace PMS.Infrastructure.Repository
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly PmsDbContext _context;



        public UserProfileRepository(PmsDbContext context)
        {
            _context = context;
        }



        public async Task<UserProfile> GetAsync(Guid id)
            => await Task.FromResult(await _context.UserProfiles.SingleOrDefaultAsync(u => u.Id == id) 
                ?? throw new UserNotFoundException($"UserProfile with ID '{id}' DOES NOT EXIST"));

        public async Task CreateAsync(UserProfile userProfile)
        {
            await _context.UserProfiles.AddAsync(userProfile);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserProfile userProfile)
        {
            _context.UserProfiles.Update(userProfile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserProfile userProfile)
        {
            _context.UserProfiles.Remove(userProfile);
            await _context.SaveChangesAsync();
        }
    }
}
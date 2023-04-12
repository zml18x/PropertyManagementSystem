using PMS.Core.Entities;

namespace PMS.Core.Repository
{
    public interface IUserRepository
    {
        public Task<User> GetAsync(Guid id);
        public Task<User> GetAsync(string email);
        public Task CreateAsync(User user);
        public Task UpdateAsync(User user);
        public Task DeleteAsync(User user);
    }
}
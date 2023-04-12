using PMS.Infrastructure.Dto;

namespace PMS.Infrastructure.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> GetAsync(Guid id);
        public Task RegisterAsync(string email, string password, string firstName, string lastName, string phoneNumber);
        public Task LoginAsync(string email, string password);
    }
}
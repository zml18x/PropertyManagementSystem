using PMS.Core.Entities;
using PMS.Core.Repository;
using PMS.Infrastructure.Exceptions;

namespace PMS.Infrastructure.Extensions
{
    public static class UserRepositoryExtensions
    {
        public static async Task<User> GetOrFailAsync(this IUserRepository repository,Guid id)
        {
            var user = await repository.GetAsync(id);

            if (user == null)
                throw new UserNotFoundException($"User with ID '{id}' does not exist");

            return user;
        }
    }
}
using PMS.Core.Entities;
using PMS.Core.Repository;
using PMS.Infrastructure.Exceptions;

namespace PMS.Infrastructure.Extensions
{
    public static class AddressRepositoryExtensions
    {
        public static async Task<Address> GetOrFailAsync(this IAddressRepository repository, Guid id)
        {
            var address = await repository.GetAsync(id);

            if (address == null)
                throw new AddressNotFoundException($"Address with ID '{id}' does not exist");

            return address;
        }
    }
}
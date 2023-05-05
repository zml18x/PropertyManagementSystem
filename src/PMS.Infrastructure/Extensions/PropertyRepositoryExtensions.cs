using PMS.Core.Entities;
using PMS.Core.Repository;
using PMS.Infrastructure.Exceptions;

namespace PMS.Infrastructure.Extensions
{
    public static class PropertyRepositoryExtensions
    {
        public static async Task<Property> GetOrFailAsync(this IPropertyRepository repository, Guid id)
        {
            var property = await repository.GetAsync(id);

            if (property == null)
                throw new PropertyNotFoundException($"Property with ID '{id}' does not exist");

            return property;
        }
    }
}
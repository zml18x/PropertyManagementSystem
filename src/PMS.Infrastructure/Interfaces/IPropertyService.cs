using PMS.Core.Enum;
using PMS.Infrastructure.Dto;

namespace PMS.Infrastructure.Interfaces
{
    public interface IPropertyService
    {
        public Task<PropertyDto> GetAsync(Guid id);
        public Task<IEnumerable<PropertyDto>> GetAllAsync(Guid userId);
        public Task CreateAsync(Guid propertyId,Guid userId, Guid addressId, string name, string description, PropertyType type);
    }
}
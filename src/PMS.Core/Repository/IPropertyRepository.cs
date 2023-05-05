using PMS.Core.Entities;

namespace PMS.Core.Repository
{
    public interface IPropertyRepository
    {
        public Task<Property> GetAsync(Guid id);
        public Task<Property> GetAsync(Guid id, string name);
        public Task<IEnumerable<Property>> GetAllAsync(Guid userId);
        public Task CreateAsync(Property property);
        public Task UpdateAsync(Property property);
        public Task DeleteAsync(Property property);
    }
}
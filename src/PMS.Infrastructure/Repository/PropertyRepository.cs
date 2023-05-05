using Microsoft.EntityFrameworkCore;
using PMS.Core.Entities;
using PMS.Core.Repository;
using PMS.Infrastructure.Data.Context;

namespace PMS.Infrastructure.Repository
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly PmsDbContext _context;



        public PropertyRepository(PmsDbContext context)
        {
            _context = context;
        }



        public async Task<Property> GetAsync(Guid id)
            => await Task.FromResult(await _context.Properties.SingleOrDefaultAsync(p => p.Id == id));

        public async Task<Property> GetAsync(Guid id,string name)
            => await Task.FromResult(await _context.Properties.SingleOrDefaultAsync(p => p.Id == id && p.Name == name));

        public async Task<IEnumerable<Property>> GetAllAsync(Guid userId)
            => await Task.FromResult(_context.Properties.Where(p => p.UserId == userId));

        public async Task CreateAsync(Property property)
        {
            await _context.Properties.AddAsync(property);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Property property)
        {
            _context.Properties.Update(property);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Property property)
        {
            _context.Properties.Remove(property);
            await _context.SaveChangesAsync();
        }  
    }
}
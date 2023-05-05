using PMS.Core.Entities;
using PMS.Core.Repository;
using PMS.Infrastructure.Data.Context;

namespace PMS.Infrastructure.Repository
{
    public class AddressRepository : IAddressRepository
    {
        private readonly PmsDbContext _context;



        public AddressRepository(PmsDbContext context)
        {
            _context = context;
        }



        public async Task<Address> GetAsync(Guid id)
            => await Task.FromResult(_context.Addresses.SingleOrDefault(a => a.Id == id));

        public async Task CreateAsync(Address address)
        {
            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Address address)
        {
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Address address)
        {
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
        }
    }
}
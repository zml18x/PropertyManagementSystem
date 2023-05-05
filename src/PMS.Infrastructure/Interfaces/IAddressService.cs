using PMS.Core.Enum;
using PMS.Infrastructure.Dto;

namespace PMS.Infrastructure.Interfaces
{
    public interface IAddressService
    {
        public Task<AddressDto> GetAsync(Guid id);
        public Task CreateAsync(Guid addressId,string country, string city, string postalCode, string street, string buildingNumber, AddressType addressType);
    }
}
using PMS.Core.Entities;
using PMS.Core.Enum;
using PMS.Core.Repository;
using PMS.Infrastructure.Dto;
using PMS.Infrastructure.Extensions;
using PMS.Infrastructure.Interfaces;

namespace PMS.Infrastructure.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;



        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }



        public async Task<AddressDto> GetAsync(Guid id)
        {
            var address = await _addressRepository.GetOrFailAsync(id);

            return new AddressDto(address.Id, address.Country, address.City, address.PostalCode, address.Street, address.BuildingNumber);
        }

        public async Task CreateAsync(Guid addressId,string country, string city, string postalCode, string street, string buildingNumber, AddressType addressType)
        {
            var address = new Address(addressId, country, city, postalCode, street, buildingNumber, addressType);

            await _addressRepository.CreateAsync(address);
        }
    }
}
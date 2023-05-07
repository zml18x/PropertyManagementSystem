using PMS.Core.Enum;
using PMS.Core.Entities;
using PMS.Core.Repository;
using PMS.Infrastructure.Dto;
using PMS.Infrastructure.Extensions;
using PMS.Infrastructure.Interfaces;

namespace PMS.Infrastructure.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;



        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }



        public async Task<PropertyDto> GetAsync(Guid id)
        {
            var property = await _propertyRepository.GetOrFailAsync(id);

            return new PropertyDto(property.Id, property.AddressId, property.UserId, property.Name, property.Description, property.Type, property.RoomsCount);
        }

        public async Task<IEnumerable<PropertyDto>> GetAllAsync(Guid userId)
        {
            var properties = await _propertyRepository.GetAllAsync(userId);

            if(properties == null)
                throw new ArgumentNullException(nameof(properties));

            var propertiesDtoList = new List<PropertyDto>();

            foreach( var property in properties)
                propertiesDtoList.Add(new PropertyDto(property.Id, property.AddressId, property.UserId, property.Name, property.Description, property.Type, property.RoomsCount));

            return propertiesDtoList;
        }

        public async Task CreateAsync(Guid propertyId, Guid userId, Guid addressId, string name, string description, PropertyType type)
        {
            var property = await _propertyRepository.GetAsync(userId, name);

            if (property != null)
                throw new Exception();

            property = new Property(propertyId, userId, addressId, name, description, type);

            await _propertyRepository.CreateAsync(property);
        }
    }
}
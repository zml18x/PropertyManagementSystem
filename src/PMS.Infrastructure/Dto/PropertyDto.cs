using PMS.Core.Enum;

namespace PMS.Infrastructure.Dto
{
    public class PropertyDto
    {
        public Guid PropertyId { get; set; }
        public Guid AddressId { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PropertyType Type { get; set; }
        public int RoomsCount { get; set; }


        public PropertyDto(Guid propertyId, Guid addressId, Guid userId, string name, string description, PropertyType type, int roomsCount)
        {
            PropertyId = propertyId;
            AddressId = addressId;
            UserId = userId;
            Name = name;
            Description = description;
            Type = type;
            RoomsCount = roomsCount;
        }
    }
}
using PMS.Core.Enum;

namespace PMS.Core.Entities
{
    public class Address : Entity
    {
        public string Country { get; protected set; }
        public string City { get; protected set; }
        public string PostalCode { get; protected set; }
        public string Street { get; protected set; }
        public string BuildingNumber { get; protected set; }
        public AddressType AddressType { get; protected set; }


        protected Address() { }
        public Address(Guid id, string country, string city, string postalCode, string street, string buildingNumber, AddressType addressType)
        {
            Id = id;
            Country = country;
            City = city;
            PostalCode = postalCode;
            Street = street;
            BuildingNumber = buildingNumber;
            AddressType = addressType;
        }
    }
}
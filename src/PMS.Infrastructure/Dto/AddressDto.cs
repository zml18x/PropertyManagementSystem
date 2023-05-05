namespace PMS.Infrastructure.Dto
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }


        public AddressDto(Guid id, string country, string city, string postalCode, string street, string buildingNumber)
        {
            Id = id;
            Country = country;
            City = city;
            PostalCode = postalCode;
            Street = street;
            BuildingNumber = buildingNumber;
        }
    }
}
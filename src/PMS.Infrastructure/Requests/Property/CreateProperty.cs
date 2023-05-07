using System.ComponentModel.DataAnnotations;

namespace PMS.Infrastructure.Requests.Property
{
    public class CreateProperty
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Type { get; set; }

        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string BuildingNumber { get; set; }


        public CreateProperty(string name, string description, string type,
            string country, string city, string postalCode, string street, string buildingNumber)
        {
            Name = name;
            Description = description;
            Type = type;

            Country = country;
            City = city;
            PostalCode = postalCode;
            Street = street;
            BuildingNumber = buildingNumber;
        }
    }
}
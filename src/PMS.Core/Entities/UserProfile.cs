namespace PMS.Core.Entities
{
#pragma warning disable CS8618
    public class UserProfile : Entity
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string PhoneNumber { get; protected set; }
        public Guid AddressId { get; protected set; }
        public bool IsActive => !DeletedAt.HasValue;



        protected UserProfile() { }
        public UserProfile(Guid id, string firstName, string lastName, string phoneNumber, Guid addressId)
        {
            Id = id;
            SetNames(firstName, lastName);
            SetPhoneNumber(phoneNumber);
            AddressId = addressId;
        }



        private void SetNames(string firstName, string lastName)
        {
            // VALIDATION

            FirstName = firstName;
            LastName = lastName;
        }

        private void SetPhoneNumber(string phoneNumber)
        {
            // VALIDATION

            PhoneNumber = phoneNumber;
        }
    }
#pragma warning restore CS8618
}
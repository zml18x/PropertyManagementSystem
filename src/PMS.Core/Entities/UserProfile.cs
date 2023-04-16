namespace PMS.Core.Entities
{
    public class UserProfile : Entity
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string PhoneNumber { get; protected set; }
        public bool IsActive => !DeletedAt.HasValue;



        protected UserProfile() { }
        public UserProfile(Guid id, string firstName, string lastName, string phoneNumber)
        {
            Id = id;
            SetNames(firstName, lastName);
            SetPhoneNumber(phoneNumber);
        }



        private void SetNames(string firstName, string lastName)
        {
            // VALIDATION

            if(firstName == null || lastName == null)
                throw new ArgumentNullException(nameof(firstName));

            FirstName = firstName;
            LastName = lastName;
        }

        private void SetPhoneNumber(string phoneNumber)
        {
            // VALIDATION

            if(phoneNumber == null)
                throw new ArgumentNullException(nameof(phoneNumber));

            PhoneNumber = phoneNumber;
        }
    }
}
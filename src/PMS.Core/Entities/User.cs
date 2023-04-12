namespace PMS.Core.Entities
{
    public class User : Entity
    {
        public string Role { get; protected set; }
        public string Email { get; protected set; }
        public byte[] PasswordHash { get; protected set; }
        public byte[] PasswordSalt { get; protected set; }
        public Guid UserProfileId { get; protected set; }
        public bool IsActive => !DeletedAt.HasValue;



        protected User() { }
        public User(Guid id,Guid userProfileId, string email, byte[] passwordHash, byte[] passwordSalt, string role = "User")
        {
            SetId(id, userProfileId);
            SetEmail(email);
            SetPassword(passwordHash, passwordSalt);
            SetRole(role);
        }



        private void SetPassword(byte[] passwordHash, byte[] passwordSalt)
        {
            // VALIDATION

            PasswordHash = passwordHash; 
            PasswordSalt = passwordSalt;
        }

        private void SetId(Guid id, Guid userProfileId)
        {
            // VALIDATION

            Id = id;
            UserProfileId = userProfileId;
        }

        private void SetRole(string role)
        {
            // VALIDATION

            Role = role;
        }

        private void SetEmail(string email)
        {
            // VALIDATION

            Email = email;
        }
    }
}
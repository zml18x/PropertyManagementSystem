namespace PMS.Infrastructure.Dto
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public Guid UserProfileId { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }
}
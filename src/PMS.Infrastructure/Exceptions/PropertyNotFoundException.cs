namespace PMS.Infrastructure.Exceptions
{
    public class PropertyNotFoundException : Exception
    {
        public PropertyNotFoundException(string? message) : base(message) { }
    }
}
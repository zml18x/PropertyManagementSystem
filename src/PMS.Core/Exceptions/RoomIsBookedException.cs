namespace PMS.Core.Exceptions
{
    public class RoomIsBookedException : Exception
    {
        public RoomIsBookedException(string? message) : base(message) { }
    }
}
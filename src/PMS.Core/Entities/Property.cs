using PMS.Core.Enum;

namespace PMS.Core.Entities
{
    public class Property : Entity
    {
        private ISet<Room> _rooms = new HashSet<Room>();
        public Guid AddressId { get; protected set; }
        public Guid UserId { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public PropertyType Type { get; protected set; }
        public IEnumerable<Room> Rooms => _rooms;
        public IEnumerable<Room> AvailableRooms => _rooms.Where(r => r.Availability == true);
        public int RoomsCount => _rooms.Count;
        public int AvailableRoomsCount => AvailableRooms.Count();
        public int ReservedRoomsCount => Rooms.Except(AvailableRooms).Count();



        protected Property() { }
        public Property(Guid id, Guid userId, Guid addressId, string name, string description, PropertyType type)
        {
            Id = id;
            UserId = userId;
            AddressId = addressId;
            Name = name;
            Description = description;
            Type = type;
        }



        public void AddRooms(int amount)
        {
            throw new NotImplementedException();
        }
    }
}
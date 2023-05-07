using PMS.Core.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Core.Entities
{
    public class Property : Entity
    {
        [NotMapped]
        private ISet<Room> _rooms = new HashSet<Room>();
        public Guid AddressId { get; protected set; }
        public Guid UserId { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public PropertyType Type { get; protected set; }
        [NotMapped]
        public IEnumerable<Room> Rooms => _rooms;
        [NotMapped]
        public IEnumerable<Room> AvailableRooms => _rooms.Where(r => r.Availability == true);
        [NotMapped]
        public int RoomsCount => _rooms.Count;
        [NotMapped]
        public int AvailableRoomsCount => AvailableRooms.Count();
        [NotMapped]
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
    }
}
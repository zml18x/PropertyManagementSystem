﻿using PMS.Core.Enum;
using PMS.Core.Exceptions;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Core.Entities
{
    public class Room : Entity
    {
        public Guid PropertyId { get; protected set; }
        public RoomType RoomType { get; protected set; }
        public int RoomNumber { get; protected set; }
        public int FloorNumber { get; protected set; }
        public int MaxOccupancy { get; protected set; }
        public string? Name { get; protected set; }
        public string? Description { get; protected set; }
        [NotMapped]
        public bool Availability => !CheckInDate.HasValue;
        public DateTime? CheckInDate { get; protected set; }
        public DateTime? CheckOutDate { get; protected set; }



        protected Room() { }
        public Room(Property property,Guid id,RoomType roomType, int roomNumber, int floorNumber, int maxOccupancy,
            string? name = null, string? description = null)
        {
            Id = id;
            PropertyId = property.Id;
            RoomType = roomType;
            RoomNumber = roomNumber;
            FloorNumber = floorNumber;
            MaxOccupancy = maxOccupancy;
            Name = name;
            Description = description;
        }



        public void ReserveRoom(DateTime checkInDate, DateTime? checkOutDate = null)
        {
            if (!Availability)
                throw new RoomIsBookedException("The room is already booked");

            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
        }

        public void ReleaseRoom()
        {
            CheckInDate = null;
            CheckOutDate = null;
        }
    }
}
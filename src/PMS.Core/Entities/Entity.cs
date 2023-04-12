﻿namespace PMS.Core.Entities
{
    public abstract class Entity
    {
        public virtual Guid Id { get; protected set; }
        public virtual DateTime CreatedAt { get; protected set; }
        public virtual DateTime LastUpdatedAt { get; protected set; }
        public virtual DateTime? DeletedAt { get; protected set; }



        protected Entity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            LastUpdatedAt = DateTime.UtcNow;
        }
    }
}
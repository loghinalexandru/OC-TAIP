using System;
using System.Collections.Generic;

namespace AccelerometerStorage.Domain
{
    public abstract class Entity
    {
        protected Entity()
        {
            this.Id = Guid.NewGuid();
            this._domainEvents = new List<INotification>();
        }

        public Guid Id { get; private set; }

        private readonly List<INotification> _domainEvents;

        public IReadOnlyList<INotification> DomainEvents => _domainEvents;

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }

            if (obj == null)
            {
                return false;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            Entity other = (Entity) obj;
            return this.GetHashCode() == other.GetHashCode();
        }

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void RemoteDomainEvent(INotification eventItem)
        {
            _domainEvents.Remove(eventItem);
        }
    }
}
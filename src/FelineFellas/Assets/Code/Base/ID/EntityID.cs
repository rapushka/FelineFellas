using System;

namespace FelineFellas
{
    [Serializable]
    public readonly struct EntityID : IEquatable<EntityID>
    {
        public int ID { get; }

        public EntityID(int id) => ID = id;

        public static implicit operator int(EntityID entityID) => entityID.ID;

        public static bool operator ==(EntityID lhs, EntityID rhs) => lhs.Equals(rhs);
        public static bool operator !=(EntityID lhs, EntityID rhs) => !(lhs == rhs);

        public override string ToString()
            => this.TryGetEntity(out var entity) ? $"{ID} {entity.GetName()}" : "DESTROYED";

#region Boilerplate
        public override bool Equals(object obj) => obj is EntityID other && Equals(other);

        public bool Equals(EntityID other) => GetHashCode() == other.GetHashCode();

        public override int GetHashCode() => ID;
#endregion
    }
}
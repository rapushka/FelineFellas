using System;

namespace FelineFellas
{
    public readonly struct StageID : IEquatable<StageID>
    {
        /// starts with 1
        public int Number { get; }

        public StageID(int number) => Number = number;

        public static implicit operator int(StageID entityID) => entityID.Number;

        public static bool operator ==(StageID lhs, StageID rhs) => lhs.Equals(rhs);
        public static bool operator !=(StageID lhs, StageID rhs) => !(lhs == rhs);

        public override string ToString()
            => $"number: {Number}";

#region Boilerplate
        public override bool Equals(object obj) => obj is StageID other && Equals(other);

        public bool Equals(StageID other) => GetHashCode() == other.GetHashCode();

        public override int GetHashCode() => Number;
#endregion
    }
}
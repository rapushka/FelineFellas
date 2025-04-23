using System;
using SmartIdTable;

namespace FelineFellas
{
    [Serializable]
    public struct CardIDRef : IEquatable<CardIDRef>
    {
        [IdRef(startsWith: Constants.TableID.Cards)]
        public string Value;

        public static implicit operator CardIDRef(string unitID) => new() { Value = unitID };
        public static implicit operator string(CardIDRef cardID) => cardID.Value;

        public static bool operator ==(CardIDRef lhs, CardIDRef rhs) => lhs.Value == rhs.Value;
        public static bool operator !=(CardIDRef lhs, CardIDRef rhs) => !(lhs == rhs);

#region Boilerplate
        public bool Equals(CardIDRef other) => Value == other.Value;

        public override bool Equals(object obj) => obj is CardIDRef other && Equals(other);

        public override int GetHashCode() => (Value != null ? Value.GetHashCode() : 0);
#endregion

        public override string ToString() => Value;
    }
}
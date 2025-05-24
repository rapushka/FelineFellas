using Entitas.Generic;

namespace FelineFellas
{
    public static class DebugNameExtensions
    {
        public static string ToShortString(this Entity<GameScope> @this)
            => $"{@this.ID().ID} {@this.GetName()}";

        public static string GetName(this Entity<GameScope> @this)
            => @this.ToString<Name, string>(defaultValue: "e");
    }
}
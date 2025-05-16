using Entitas.Generic;

namespace FelineFellas
{
    public static class DebugNameExtensions
    {
        public static string GetName(this Entity<GameScope> @this)
            => @this.ToString<Name, string>(defaultValue: "e");
    }
}
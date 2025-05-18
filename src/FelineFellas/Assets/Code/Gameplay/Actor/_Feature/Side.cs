using Entitas.Generic;

namespace FelineFellas
{
    public enum Side
    {
        Unknown = 0,
        Player = 1,
        Enemy = 2,
    }

    public static class SideExtensions
    {
        public static bool OnSameSide(this Entity<GameScope> @this, Entity<GameScope> other)
            => @this.Get<OnSide>().Value == other.Get<OnSide>().Value;
    }
}
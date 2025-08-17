using Entitas.Generic;

namespace FelineFellas
{
    public static class ValidEntityExtensions
    {
        public static bool IsValid(this Entity<GameScope> @this)
            => @this.isEnabled
                && !@this.Is<Destroy>();

        public static bool IsAlive(this Entity<GameScope> @this)
            => @this.IsValid()
                && !@this.Is<Dead>()
                && !@this.Is<Defeated>();
    }
}
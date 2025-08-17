using Entitas.Generic;

namespace FelineFellas
{
    public static class ValidEntityExtensions
    {
        public static bool IsAlive(this Entity<GameScope> @this)
            => @this.isEnabled
                && !@this.Is<Dead>()
                && !@this.Is<Destroy>()
                && !@this.Is<Defeated>();
    }
}
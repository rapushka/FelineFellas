using Entitas.Generic;

namespace FelineFellas
{
    public static class ChildrenExtensions
    {
        public static Entity<GameScope> Parent(this Entity<GameScope> @this)
            => @this.Get<ChildOf>().Value.GetEntity();
    }
}
using Entitas.Generic;

namespace FelineFellas
{
    public static class ChildrenExtensions
    {
        public static Entity<GameScope> Parent(this Entity<GameScope> @this)
            => @this.Get<ChildOf>().Value.GetEntity();

        public static Entity<GameScope> SetParent(this Entity<GameScope> @this, Entity<GameScope> newParent)
            => @this.Set<ChildOf, EntityID>(newParent.ID());
    }
}
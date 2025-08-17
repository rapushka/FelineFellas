using Entitas.Generic;

namespace FelineFellas
{
    public static partial class ActorHierarchyDataExtensions
    {
        public static ActorHierarchyData Hierarchy(this Entity<GameScope> @this)
            => @this.Get<ActorHierarchy>().Value;
    }
}
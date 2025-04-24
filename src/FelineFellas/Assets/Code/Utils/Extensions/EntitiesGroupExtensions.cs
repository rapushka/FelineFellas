using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public static class EntitiesGroupExtensions
    {
        public static Entity<TScope> First<TScope>(this IGroup<Entity<TScope>> @this)
            where TScope : IScope
        {
            foreach (var entity in @this)
                return entity;

            throw new("The Group is empty");
        }
    }
}
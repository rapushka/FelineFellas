using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public static class EntitiesGroupExtensions
    {
        public static bool TryGetFirst<TScope>(this IGroup<Entity<TScope>> @this, out Entity<TScope> result)
            where TScope : IScope
        {
            foreach (var entity in @this)
            {
                result = entity;
                return true;
            }

            result = null;
            return false;
        }
    }
}
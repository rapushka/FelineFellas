using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public static class EntitiesGroupExtensions
    {
        public static bool Any<TScope>(this IGroup<Entity<TScope>> @this)
            where TScope : IScope
            => @this.count > 0;

        public static bool TryGetFirst<TScope>(this IGroup<Entity<TScope>> @this, out Entity<TScope> result)
            where TScope : IScope
        {
            result = @this.FirstOrDefault();
            return result is not null;
        }

        public static Entity<TScope> First<TScope>(this IGroup<Entity<TScope>> @this)
            where TScope : IScope
            => @this.FirstOrDefault() ?? throw new("Group is Empty!");

        public static Entity<TScope> FirstOrDefault<TScope>(this IGroup<Entity<TScope>> @this)
            where TScope : IScope
        {
            foreach (var entity in @this)
                return entity;

            return null;
        }
    }
}
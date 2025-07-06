using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public static class EntitiesGroupExtensions
    {
        public static bool Any<TScope>(this IGroup<Entity<TScope>> @this)
            where TScope : IScope
            => @this.count > 0;

        public static bool Any<TScope>(this IGroup<Entity<TScope>> @this, Func<Entity<TScope>, bool> predicate)
            where TScope : IScope
        {
            foreach (var entity in @this)
            {
                if (predicate.Invoke(entity))
                    return true;
            }

            return false;
        }

        public static bool TryGetFirst<TScope>(this IGroup<Entity<TScope>> @this, out Entity<TScope> result)
            where TScope : IScope
        {
            result = @this.FirstOrDefault();
            return result is not null;
        }

        public static Entity<TScope> First<TScope>(this IGroup<Entity<TScope>> @this)
            where TScope : IScope
            => @this.FirstOrDefault() ?? throw new("Group is Empty!");

        public static Entity<TScope> First<TScope>(this IGroup<Entity<TScope>> @this, Func<Entity<TScope>, bool> predicate)
            where TScope : IScope
        {
            foreach (var entity in @this)
            {
                if (predicate.Invoke(entity))
                    return entity;
            }

            throw new("No Such Entity!");
        }

        public static Entity<TScope> FirstOrDefault<TScope>(this IGroup<Entity<TScope>> @this)
            where TScope : IScope
        {
            foreach (var entity in @this)
                return entity;

            return null;
        }

        public static IEnumerable<Entity<TScope>> Where<TScope>(this IGroup<Entity<TScope>> @this, Func<Entity<TScope>, bool> predicate)
            where TScope : IScope
        {
            foreach (var entity in @this)
            {
                if (predicate.Invoke(entity))
                    yield return entity;
            }
        }

        public static int Count<TScope>(this IGroup<Entity<TScope>> @this, Func<Entity<TScope>, bool> predicate)
            where TScope : IScope
        {
            var counter = 0;

            foreach (var entity in @this)
            {
                if (predicate.Invoke(entity))
                    counter++;
            }

            return counter;
        }

        public static IEnumerable<Entity<GameScope>> With<TComponent>(this IGroup<Entity<GameScope>> @this)
            where TComponent : IComponent, IInScope<GameScope>, new()
            => @this.Where(e => e.Has<TComponent>());

        public static IEnumerable<Entity<GameScope>> With<TComponent>(this IEnumerable<Entity<GameScope>> @this)
            where TComponent : IComponent, IInScope<GameScope>, new()
            => @this.With<GameScope, TComponent>();

        public static IEnumerable<Entity<TScope>> With<TScope, TComponent>(this IEnumerable<Entity<TScope>> @this)
            where TScope : IScope
            where TComponent : IComponent, IInScope<TScope>, new()
            => @this.Where(e => e.Has<TComponent>());
    }
}
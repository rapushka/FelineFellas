using System;
using Entitas.Generic;
using JetBrains.Annotations;

namespace FelineFellas
{
    public static class ComponentExtensions
    {
        public static TValue Pop<TComponent, TValue>(this Entity<GameScope> @this)
            where TComponent : ValueComponent<TValue>, IInScope<GameScope>, new()
        {
            var value = @this.Get<TComponent>().Value;
            @this.Remove<TComponent>();

            return value;
        }

        public static Entity<GameScope> Add<TComponent, TValue>(this Entity<GameScope> @this, Func<TValue> getValue, bool @if)
            where TComponent : ValueComponent<TValue>, IInScope<GameScope>, new()
        {
            if (@if)
                @this.Add<TComponent, TValue>(getValue.Invoke());

            return @this;
        }

        public static Entity<GameScope> Add<TComponent, TValue>(this Entity<GameScope> @this, TValue value, bool @if)
            where TComponent : ValueComponent<TValue>, IInScope<GameScope>, new()
        {
            if (@if)
                @this.Add<TComponent, TValue>(value);

            return @this;
        }

        public static Entity<GameScope> Set<TComponent, TValue>(this Entity<GameScope> @this, TValue value, bool @if)
            where TComponent : ValueComponent<TValue>, IInScope<GameScope>, new()
        {
            if (@if)
                @this.Set<TComponent, TValue>(value);

            return @this;
        }

        public static Entity<GameScope> AddIfNotNull<TComponent, TValue>(this Entity<GameScope> @this, [CanBeNull] TValue value)
            where TComponent : ValueComponent<TValue>, IInScope<GameScope>, new()
        {
            if (value is not null)
                @this.Add<TComponent, TValue>(value);

            return @this;
        }

        public static Entity<GameScope> AddIfNotNull<TComponent, TValue>(this Entity<GameScope> @this, TValue? value)
            where TComponent : ValueComponent<TValue>, IInScope<GameScope>, new()
            where TValue : struct
        {
            if (value is not null)
                @this.Add<TComponent, TValue>(value.Value);

            return @this;
        }
    }
}
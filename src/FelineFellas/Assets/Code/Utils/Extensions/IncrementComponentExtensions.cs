using Entitas.Generic;

namespace FelineFellas
{
    public static class IncrementComponentExtensions
    {
        public static Entity<GameScope> Decrement<TComponent>(this Entity<GameScope> @this, float value)
            where TComponent : ValueComponent<float>, IInScope<GameScope>, new()
            => @this.Increment<TComponent>(value * -1);

        public static Entity<GameScope> Increment<TComponent>(this Entity<GameScope> @this, float value)
            where TComponent : ValueComponent<float>, IInScope<GameScope>, new()
        {
            var oldValue = @this.Get<TComponent, float>();
            return @this.Set<TComponent, float>(oldValue + value);
        }

        public static Entity<GameScope> Decrement<TComponent>(this Entity<GameScope> @this, int value)
            where TComponent : ValueComponent<int>, IInScope<GameScope>, new()
            => @this.Increment<TComponent>(value * -1);

        public static Entity<GameScope> Increment<TComponent>(this Entity<GameScope> @this, int value)
            where TComponent : ValueComponent<int>, IInScope<GameScope>, new()
        {
            var oldValue = @this.Get<TComponent, int>();
            return @this.Set<TComponent, int>(oldValue + value);
        }
    }
}
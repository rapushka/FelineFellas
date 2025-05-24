using Entitas.Generic;

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
    }
}
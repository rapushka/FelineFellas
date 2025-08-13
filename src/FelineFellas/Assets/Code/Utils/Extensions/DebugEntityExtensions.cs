using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public static class DebugEntityExtensions
    {
        public static void AssertIs<TComponent>(this Entity<GameScope> @this)
            where TComponent : IComponent, IInScope<GameScope>, new()
        {
#if DEBUG
            if (!@this.Has<TComponent>())
                UnityEngine.Debug.LogError($"{@this} is not {typeof(TComponent).Name}!");
#endif
        }
    }
}
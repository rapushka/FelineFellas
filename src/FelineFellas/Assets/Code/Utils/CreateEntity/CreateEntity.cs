using Entitas.Generic;

namespace FelineFellas
{
    public static class CreateEntity
    {
        public static Entity<GameScope> Empty()
            => Contexts.Instance.Get<GameScope>().CreateEntity();
    }
}
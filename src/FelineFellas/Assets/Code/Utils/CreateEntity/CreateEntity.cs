using Entitas.Generic;

namespace FelineFellas
{
    public static class CreateEntity
    {
        private static IIdentifiesService Identifies => ServiceLocator.Resolve<IIdentifiesService>();

        public static Entity<GameScope> Empty()
            => Contexts.Instance.Get<GameScope>().CreateEntity()
                .Add<ID, EntityID>(new(Identifies.Next()));
    }
}
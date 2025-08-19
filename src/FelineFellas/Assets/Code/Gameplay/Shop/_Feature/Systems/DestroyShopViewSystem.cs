using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class DestroyShopViewSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<StageCompletedEvent>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _shops
            = GroupBuilder<GameScope>
                .With<Shop>()
                .Build();

        public void Execute()
        {
            foreach (var _ in _events)
            foreach (var shop in _shops)
            {
                shop.Add<Destroy>();
            }
        }
    }
}
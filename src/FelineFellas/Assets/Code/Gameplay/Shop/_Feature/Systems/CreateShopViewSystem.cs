using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CreateShopViewSystem : IInitializeSystem
    {
        private readonly IGroup<Entity<GameScope>> _levels
            = GroupBuilder<GameScope>
                .With<Level>()
                .Build();

        private static IShopFactory ShopFactory => ServiceLocator.Resolve<IShopFactory>();

        public void Initialize()
        {
            foreach (var level in _levels)
            {
                ShopFactory.Create()
                    .Add<ChildOf, EntityID>(level.ID());
            }
        }
    }
}
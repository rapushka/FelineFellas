using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CreateShopViewSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _levels
            = GroupBuilder<GameScope>
                .With<Stage>()
                .And<EnteringStage>()
                .Build();

        private static IShopFactory ShopFactory => ServiceLocator.Resolve<IShopFactory>();

        public void Execute()
        {
            foreach (var level in _levels)
            {
                ShopFactory.Create()
                    .Add<ChildOf, EntityID>(level.ID());
            }
        }
    }
}
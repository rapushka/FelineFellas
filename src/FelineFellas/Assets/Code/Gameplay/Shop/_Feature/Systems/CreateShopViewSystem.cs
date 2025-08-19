using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CreateShopViewSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _stages
            = GroupBuilder<GameScope>
                .With<Stage>()
                .And<EnteringStage>()
                .Build();

        private static IShopFactory ShopFactory => ServiceLocator.Resolve<IShopFactory>();

        public void Execute()
        {
            foreach (var stage in _stages)
            {
                ShopFactory.Create()
                    .Add<ChildOf, EntityID>(stage.ID());
            }
        }
    }
}
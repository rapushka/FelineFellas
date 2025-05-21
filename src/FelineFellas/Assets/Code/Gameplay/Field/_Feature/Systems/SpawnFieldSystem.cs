using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class SpawnFieldSystem : IInitializeSystem
    {
        private readonly IGroup<Entity<GameScope>> _levels
            = GroupBuilder<GameScope>
                .With<Level>()
                .Build();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static IFieldFactory FieldFactory => ServiceLocator.Resolve<IFieldFactory>();

        private static FieldConfig FieldConfig => GameConfig.Field;

        public void Initialize()
        {
            foreach (var level in _levels)
            {
                FieldFactory.CreateField(FieldConfig.View.FieldCenter)
                    .Add<ChildOf, EntityID>(level.ID());
            }
        }
    }
}
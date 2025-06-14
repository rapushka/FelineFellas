using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class SpawnFieldSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _stages
            = GroupBuilder<GameScope>
                .With<Stage>()
                .And<EnteringStage>()
                .Build();

        private static IFieldFactory FieldFactory => ServiceLocator.Resolve<IFieldFactory>();

        public void Execute()
        {
            foreach (var stage in _stages)
            {
                FieldFactory.CreateField()
                    .Add<ChildOf, EntityID>(stage.ID());
            }
        }
    }
}
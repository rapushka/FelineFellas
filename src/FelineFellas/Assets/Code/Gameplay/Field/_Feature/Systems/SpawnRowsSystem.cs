using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class SpawnRowsSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _stages
            = GroupBuilder<GameScope>
                .With<Stage>()
                .And<EnteringStage>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _actors
            = GroupBuilder<GameScope>
                .With<Actor>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _fields
            = GroupBuilder<GameScope>
                .With<Field>()
                .Build();

        private static IFieldFactory FieldFactory => ServiceLocator.Resolve<IFieldFactory>();

        public void Execute()
        {
            foreach (var _ in _stages)
            foreach (var field in _fields)
            foreach (var actor in _actors)
            {
                var side = actor.Get<OnSide>().Value;
                FieldFactory.CreateRow(side, field)
                    .Add<ChildOf, EntityID>(field.ID());
            }
        }
    }
}
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class DestroyFieldOnStageCompletedSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<StageCompletedEvent>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _fields
            = GroupBuilder<GameScope>
                .With<Field>()
                .Build();

        public void Execute()
        {
            foreach (var _ in _events)
            foreach (var field in _fields)
            {
                field.Add<Destroy>();
            }
        }
    }
}
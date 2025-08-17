using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class OnStageCompletedEventProcessedSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<StageCompletedEvent>()
                .Build();

        public void Execute()
        {
            foreach (var e in _events)
                e.AddSafely<Destroy>();
        }
    }
}
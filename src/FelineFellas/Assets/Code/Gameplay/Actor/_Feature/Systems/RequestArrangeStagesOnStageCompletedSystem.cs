using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class RequestArrangeStagesOnStageCompletedSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<StageCompletedEvent>()
                .Build();

        public void Execute()
        {
            if (!_events.Any())
                return;

            CreateEntity.Empty()
                .Add<ArrangeStagesEvent>()
                ;
        }
    }
}
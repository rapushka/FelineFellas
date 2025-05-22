using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public class TickCardsDiscardDelaySystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<DelayBeforeStartTurn>()
                .Build();

        private static ITimeService TimeService => ServiceLocator.Resolve<ITimeService>();

        public void Execute()
        {
            foreach (var eventEntity in _events)
            {
                eventEntity.Decrement<DelayBeforeStartTurn>(TimeService.AnimationDelta);
                var timeLeft = eventEntity.Get<DelayBeforeStartTurn>().Value;

                if (!(timeLeft <= 0f))
                    continue;

                eventEntity
                    .Add<StartPlayerTurnEvent>()
                    .Add<Destroy>()
                    ;
            }
        }
    }
}
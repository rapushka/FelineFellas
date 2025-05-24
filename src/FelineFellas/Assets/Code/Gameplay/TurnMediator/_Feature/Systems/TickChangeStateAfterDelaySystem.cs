using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class TickChangeStateAfterDelaySystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _turnMediator
            = GroupBuilder<GameScope>
                .With<TurnMediator>()
                .And<ChangeStateAfter>()
                .Build();

        private static ITimeService TimeService => ServiceLocator.Resolve<ITimeService>();

        private readonly List<Entity<GameScope>> _buffer = new(4);

        public void Execute()
        {
            foreach (var turnMediator in _turnMediator.GetEntities(_buffer))
            {
                var timeLeft = turnMediator.Get<ChangeStateAfter, float>();
                timeLeft -= TimeService.AnimationDelta;
                turnMediator.Set<ChangeStateAfter, float>(timeLeft);

                if (timeLeft > 0f)
                    continue;

                turnMediator
                    .Remove<ChangeStateAfter>()
                    .Add<ToNextTurnState>()
                    ;
            }
        }
    }
}
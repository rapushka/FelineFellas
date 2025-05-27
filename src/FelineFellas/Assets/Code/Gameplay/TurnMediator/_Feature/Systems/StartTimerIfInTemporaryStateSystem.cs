using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class StartTimerIfInTemporaryStateSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _turnMediators
            = GroupBuilder<GameScope>
                .With<TurnMediator>()
                .Or<OnPlayerTurnStartedState>()
                .Or<OnPlayerTurnEndedState>()
                .Or<OnEnemyTurnStartedState>()
                .Or<OnEnemyTurnEndedState>()
                .Without<ChangeStateAfter>()
                .Build();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static TurnsConfig.TimingsConfig Timings => GameConfig.Turns.Timings;

        private readonly List<Entity<GameScope>> _buffer = new(4);

        public void Execute()
        {
            foreach (var turnMediator in _turnMediators.GetEntities(_buffer))
            {
                var transmitted
                        = StartWait<OnPlayerTurnStartedState>(turnMediator, Timings.TmpStartTurnDuration)
                        || StartWait<OnPlayerTurnEndedState>(turnMediator, Timings.TmpEndTurnDuration)
                        || StartWait<OnEnemyTurnStartedState>(turnMediator, Timings.TmpStartTurnDuration)
                        || StartWait<OnEnemyTurnEndedState>(turnMediator, Timings.TmpEndTurnDuration)
                    ;

                if (!transmitted)
                    throw new("New State isn't Temporary!");
            }
        }

        private bool StartWait<TTurnState>(Entity<GameScope> mediator, float delay)
            where TTurnState : FlagComponent, IInScope<GameScope>, new()
        {
            if (!mediator.Is<TTurnState>())
                return false;

            mediator.Add<ChangeStateAfter, float>(delay);
            return true;
        }
    }
}
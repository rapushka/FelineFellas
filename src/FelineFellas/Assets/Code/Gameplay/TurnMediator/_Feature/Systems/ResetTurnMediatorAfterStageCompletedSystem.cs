using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class ResetTurnMediatorAfterStageCompletedSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<StageCompletedEvent>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _turnMediators
            = GroupBuilder<GameScope>
                .With<TurnMediator>()
                .Build();

        public void Execute()
        {
            foreach (var _ in _events)
            foreach (var turnMediator in _turnMediators)
            {
                turnMediator
                    .RemoveSafely<ChangeStateAfter>()
                    .RemoveSafely<ToNextTurnState>()

                    // remove states
                    .RemoveSafely<OnPlayerTurnStartedState>()
                    .RemoveSafely<InDrawCardsState>()
                    .RemoveSafely<InPlayerTurnState>()
                    .RemoveSafely<OnPlayerTurnEndedState>()
                    .RemoveSafely<OnEnemyTurnStartedState>()
                    .RemoveSafely<InEnemyTurnState>()
                    .RemoveSafely<OnEnemyTurnEndedState>()
                    ;
            }
        }
    }
}
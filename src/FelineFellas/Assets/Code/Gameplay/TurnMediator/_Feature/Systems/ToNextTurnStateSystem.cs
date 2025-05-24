using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class ToNextTurnStateSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _turnMediators
            = GroupBuilder<GameScope>
                .With<TurnMediator>()
                .And<ToNextTurnState>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(4);

        public void Execute()
        {
            foreach (var turnMediator in _turnMediators.GetEntities(_buffer))
            {
                turnMediator.Remove<ToNextTurnState>();

                var transmitted
                        = Transmit<OnPlayerTurnStartedState, InDrawCardsState>(turnMediator)
                        || Transmit<InDrawCardsState, InPlayerTurnState>(turnMediator)
                        || Transmit<InPlayerTurnState, OnPlayerTurnEndedState>(turnMediator)
                        || Transmit<OnPlayerTurnEndedState, OnEnemyTurnStartedState>(turnMediator)
                        || Transmit<OnEnemyTurnStartedState, InEnemyTurnState>(turnMediator)
                        || Transmit<InEnemyTurnState, OnEnemyTurnEndedState>(turnMediator)
                        || Transmit<OnEnemyTurnEndedState, OnPlayerTurnStartedState>(turnMediator)
                    ;

                if (!transmitted)
                    throw new("Invalid Turn State Transition!");
            }
        }

        private bool Transmit<TFrom, TTo>(Entity<GameScope> mediator)
            where TFrom : FlagComponent, IInScope<GameScope>, new()
            where TTo : FlagComponent, IInScope<GameScope>, new()
        {
            if (!mediator.Is<TFrom>())
                return false;

            mediator
                .Remove<TFrom>()
                .Add<TTo>()
                ;

#if DEBUG
            // Debug.Log($"[Turn State] transition: {typeof(TFrom).Name} -> {typeof(TTo).Name}");
#endif
            return true;
        }
    }
}
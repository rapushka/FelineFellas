using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class TransitToNextTurnStateIfNoActorsDrawCardsSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _turnMediators
            = GroupBuilder<GameScope>
                .With<TurnMediator>()
                .And<InDrawCardsState>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _actorsThatDrawCards
            = GroupBuilder<GameScope>
                .With<Actor>()
                .And<DrawingCards>()
                .Build();

        public void Execute()
        {
            foreach (var mediator in _turnMediators)
            {
                var anyActorDrawsCards = _actorsThatDrawCards.Any();
                mediator.Is<ToNextTurnState>(!anyActorDrawsCards);
            }
        }
    }
}
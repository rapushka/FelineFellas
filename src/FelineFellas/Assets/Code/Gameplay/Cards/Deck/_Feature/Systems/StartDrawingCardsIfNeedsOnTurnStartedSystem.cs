using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class StartDrawingCardsIfNeedsOnTurnStartedSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<StartPlayerTurnEvent>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _decks
            = GroupBuilder<GameScope>
                .With<Deck>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _actors
            = GroupBuilder<GameScope>
                .With<Actor>()
                .And<HandSize>()
                .Build();

        public void Execute()
        {
            foreach (var _ in _events)
            foreach (var deck in _decks)
            foreach (var actor in _actors)
            {
                if (!actor.OnSameSide(deck))
                    return;

                var handIsFull = ActorUtils.IsActorHandFull(actor);
                deck.Is<DrawingCards>(!handIsFull);
            }
        }
    }
}
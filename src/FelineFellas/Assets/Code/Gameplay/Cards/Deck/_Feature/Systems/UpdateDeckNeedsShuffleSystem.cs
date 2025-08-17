using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UpdateDeckNeedsShuffleSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _actors
            = GroupBuilder<GameScope>
                .With<ActiveActor>()
                .And<DrawingCards>()
                .Without<WaitingForDeckShuffle>()
                .Build();

        public void Execute()
        {
            foreach (var actor in _actors)
            {
                var deck = actor.GetOwnedDeck();

                var deckIsEmpty = !ActorUtils.HasAnyCardInDeck(actor);
                deck.Is<NeedsShuffle>(deckIsEmpty);
            }
        }
    }
}
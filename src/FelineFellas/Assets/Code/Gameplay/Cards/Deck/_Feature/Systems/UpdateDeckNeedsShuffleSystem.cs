using System.Linq;
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
                .And<DeckOnStage>()
                .Without<WaitingForDeckShuffle>()
                .Build();

        public void Execute()
        {
            foreach (var actor in _actors)
            {
                var deck = actor.GetOwnedDeck();
                var deckID = deck.ID();

                var deckIsEmpty = !DeckUtils.GetAllCardsInDeck(deckID).Any();
                deck.Is<NeedsShuffle>(deckIsEmpty);
            }
        }
    }
}
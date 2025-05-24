using System.Linq;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UpdateDeckNeedsShuffleSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _actors
            = GroupBuilder<GameScope>
                .With<Actor>()
                .And<DrawingCardsActor>()
                .And<OwnedDeck>()
                .Without<WaitingForDeckShuffle>()
                .Build();

        public void Execute()
        {
            foreach (var actor in _actors)
            {
                var deckID = actor.Get<OwnedDeck>().Value;
                var deck = deckID.GetEntity();

                var deckIsEmpty = !DeckUtils.GetAllCardsInDeck(deckID).Any();
                deck.Is<NeedsShuffle>(deckIsEmpty);
            }
        }
    }
}
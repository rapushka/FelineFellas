using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class ShuffleDeckSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<StartTurnEvent>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _decks
            = GroupBuilder<GameScope>
                .With<Deck>()
                .And<NeedsShuffle>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _discardedCards
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<InDiscard>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(4);

        public void Execute()
        {
            foreach (var _ in _events)
            foreach (var deck in _decks.GetEntities(_buffer))
            foreach (var card in _discardedCards)
            {
                CardUtils.AddToDeck(card, deck);
                deck.Is<NeedsShuffle>(false);
            }
        }
    }
}
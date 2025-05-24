using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class SendDrawCardsOnDeckShuffleEndedSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _decks
            = GroupBuilder<GameScope>
                .With<Deck>()
                .And<ShufflingDeckTimer>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(4);

        public void Execute()
        {
            foreach (var deck in _decks.GetEntities(_buffer))
            {
                var timeLeft = deck.Get<ShufflingDeckTimer>().Value;

                if (timeLeft > 0f)
                    continue;

                deck
                    .Remove<ShufflingDeckTimer>()
                    .Is<NeedsShuffle>(false)
                    ;
            }
        }
    }
}
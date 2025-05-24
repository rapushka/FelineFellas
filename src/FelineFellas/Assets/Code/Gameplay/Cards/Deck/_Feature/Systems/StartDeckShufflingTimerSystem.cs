using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class StartDeckShufflingTimerSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _decks
            = GroupBuilder<GameScope>
                .With<Deck>()
                .And<NeedsShuffle>()
                .Without<ShufflingDeckTimer>()
                .Build();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private readonly List<Entity<GameScope>> _buffer = new(4);

        public void Execute()
        {
            foreach (var deck in _decks.GetEntities(_buffer))
            {
                deck.Add<ShufflingDeckTimer, float>(GameConfig.Turns.Timings.DeckShuffleDuration);
            }
        }
    }
}
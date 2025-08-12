using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UpdateActorWaitingForDeckShuffleSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _actors
            = GroupBuilder<GameScope>
                .With<ActiveActor>()
                .And<DrawingCards>()
                .And<OwnedDeck>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(4);

        public void Execute()
        {
            foreach (var actor in _actors.GetEntities(_buffer))
            {
                var deck = actor.Get<OwnedDeck>().Value.GetEntity();
                var deckIsShuffling = deck.Is<NeedsShuffle>() || deck.Has<ShufflingDeckTimer>();

                actor.Is<WaitingForDeckShuffle>(deckIsShuffling);
            }
        }
    }
}
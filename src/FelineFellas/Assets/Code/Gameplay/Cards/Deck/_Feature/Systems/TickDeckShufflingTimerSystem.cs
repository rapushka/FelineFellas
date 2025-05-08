using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class TickDeckShufflingTimerSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _decks
            = GroupBuilder<GameScope>
                .With<Deck>()
                .And<ShufflingDeckTimer>()
                .Build();

        private static ITimeService TimeService => ServiceLocator.Resolve<ITimeService>();

        public void Execute()
        {
            foreach (var deck in _decks)
            {
                deck.Decrement<ShufflingDeckTimer>(TimeService.AnimationDelta);
            }
        }
    }
}
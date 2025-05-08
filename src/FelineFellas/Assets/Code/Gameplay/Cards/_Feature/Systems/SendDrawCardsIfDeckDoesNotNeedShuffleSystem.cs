using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class SendDrawCardsIfDeckDoesNotNeedShuffleSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<StartTurnEvent>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _decks
            = GroupBuilder<GameScope>
                .With<Deck>()
                .Without<NeedsShuffle>()
                .Build();

        public void Execute()
        {
            // ReSharper disable once UnusedVariable
            foreach (var _ in _events)
            foreach (var deck in _decks)
            {
                CreateEntity.OneFrame()
                    .Add<DrawCardsEvent>()
                    ;
            }
        }
    }
}
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class ReturnAllPlayerCardToDeckOnStageCompletedSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _event
            = GroupBuilder<GameScope>
                .With<StageCompletedEvent>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _playerCards
            = GroupBuilder<GameScope>
                .With<PlayerCard>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _playerActors
            = GroupBuilder<GameScope>
                .With<PlayerActor>()
                .And<ActorOnStage>()
                .Build();

        public void Execute()
        {
            foreach (var _ in _event)
            foreach (var actor in _playerActors)
            {
                var deck = actor.GetOwnedDeck();

                foreach (var card in _playerCards)
                    CardUtils.SendToDeck(card, deck);
            }
        }
    }
}
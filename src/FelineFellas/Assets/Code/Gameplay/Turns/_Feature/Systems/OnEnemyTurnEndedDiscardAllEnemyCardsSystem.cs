using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class OnEnemyTurnEndedDiscardAllEnemyCardsSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<EnemyTurnEnded>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _cardsInHand
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<InHandIndex>()
                .And<EnemyCard>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(16);

        public void Execute()
        {
            foreach (var _ in _events)
            foreach (var card in _cardsInHand.GetEntities(_buffer))
            {
                CardUtils.Discard(card);
            }
        }
    }
}
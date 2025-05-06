using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UseDroppedOneShotCardsIfCanSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _droppedCards
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<WillBeUsed>()
                .And<OneShotCard>()
                .And<Dropped>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(8);

        public void Execute()
        {
            foreach (var card in _droppedCards.GetEntities(_buffer))
                CardUtils.MarkUsedAndDiscard(card);
        }
    }
}
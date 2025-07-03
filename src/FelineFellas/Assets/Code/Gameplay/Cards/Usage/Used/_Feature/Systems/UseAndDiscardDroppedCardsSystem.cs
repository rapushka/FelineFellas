using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UseAndDiscardDroppedCardsSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _droppedCards
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<WillBeUsed>()
                .And<DiscardAfterUse>()
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
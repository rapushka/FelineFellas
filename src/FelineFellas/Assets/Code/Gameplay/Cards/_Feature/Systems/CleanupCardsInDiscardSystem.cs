using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CleanupCardsInDiscardSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _cardsInDiscard
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<InDiscard>()
                .Build();

        public void Execute()
        {
            foreach (var card in _cardsInDiscard)
                CardUtils.CleanupUsedCard(card);
        }
    }
}
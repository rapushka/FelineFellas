using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class RecalculateCardsIndexesInHandSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _usedCard
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<Used>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _leftCards
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<InHandIndex>()
                .Build();

        public void Execute()
        {
            var counter = 0;

            foreach (var _ in _usedCard)
            foreach (var card in _leftCards)
            {
                card.Set<InHandIndex, int>(counter++);
            }
        }
    }
}
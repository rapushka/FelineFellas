using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UpdateSortingOrderForCardsInHandSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _cards
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<SpriteSortingGroup>()
                .And<InHandIndex>()
                .Build();

        public void Execute()
        {
            foreach (var card in _cards)
            {
                const int lowestIndex = (int)SortGroup.CardInHand;

                var cardIndex = card.Get<InHandIndex>().Value;
                card.Set<SpriteSortingIndex, int>(lowestIndex + cardIndex);
            }
        }
    }
}
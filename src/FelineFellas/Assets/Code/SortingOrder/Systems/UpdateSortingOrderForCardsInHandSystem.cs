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
                var cardIndex = card.Get<InHandIndex>().Value;
                card.SetSorting(RenderOrder.CardInHand, cardIndex);
            }
        }
    }

    public sealed class UpdateSortingOrderForDraggingCardSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _cards
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<SpriteSortingGroup>()
                .And<Dragging>()
                .Build();

        public void Execute()
        {
            foreach (var card in _cards)
            {
                card.SetSorting(RenderOrder.DraggingCard);
            }
        }
    }
}
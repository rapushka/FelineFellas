using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CheckSellDraggingCardSystem : IExecuteSystem

    {
        private readonly IGroup<Entity<GameScope>> _shops
            = GroupBuilder<GameScope>
                .With<Shop>()
                .And<SellAreaCollider>()
                .Build();

        private readonly IGroup<Entity<InputScope>> _inputs
            = GroupBuilder<InputScope>
                .With<PlayerInput>()
                .And<WorldPosition>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _draggedCard
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<Dragging>()
                .And<Price>()
                .Build();

        public void Execute()
        {
            foreach (var shop in _shops)
            foreach (var input in _inputs)
            foreach (var card in _draggedCard)
            {
                var cursorPosition = input.Get<WorldPosition>().Value;
                var fieldCollider = shop.Get<SellAreaCollider>().Value;

                var cursorOnSellArea = fieldCollider.OverlapPoint(cursorPosition);
                card.Is<WillBeSold>(cursorOnSellArea);
            }
        }
    }
}
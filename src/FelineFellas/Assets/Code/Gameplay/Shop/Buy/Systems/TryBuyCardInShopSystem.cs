using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class TryBuyCardInShopSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _slots
            = GroupBuilder<GameScope>
                .With<ShopSlot>()
                .And<CanBuy>()
                .And<PlacedCard>()
                .And<BuyButton>()
                .Build();

        private readonly IGroup<Entity<InputScope>> _inputs
            = GroupBuilder<InputScope>
                .With<PlayerInput>()
                .And<CursorJustClicked>()
                .And<WorldPosition>()
                .Build();

        public void Execute()
        {
            foreach (var input in _inputs)
            foreach (var slot in _slots)
            {
                var buyButton = slot.Get<BuyButton>().Value;

                var isMouseOverBuyButton = buyButton.OverlapPoint(input.WorldPosition());

                if (isMouseOverBuyButton)
                    slot.Add<Buy>();
            }
        }
    }
}
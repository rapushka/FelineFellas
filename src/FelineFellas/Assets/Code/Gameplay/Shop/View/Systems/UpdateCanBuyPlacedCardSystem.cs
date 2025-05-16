using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UpdateCanBuyPlacedCardSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _slots
            = GroupBuilder<GameScope>
                .With<ShopSlot>()
                .And<CanBuy>()
                .And<PlacedCard>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _players
            = GroupBuilder<GameScope>
                .With<Player>()
                .And<Money>()
                .Build();

        public void Execute()
        {
            foreach (var player in _players)
            foreach (var slot in _slots)
            {
                var playerMoney = player.Get<Money>().Value;

                var placedCard = slot.Get<PlacedCard>().Value.GetEntity();
                var cardPrice = placedCard.Get<Price>().Value;

                slot.Set<CanBuy, bool>(playerMoney >= cardPrice);
            }
        }
    }
}
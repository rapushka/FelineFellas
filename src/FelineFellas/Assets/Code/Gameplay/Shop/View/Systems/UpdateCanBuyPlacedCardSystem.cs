using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UpdateCanBuyPlacedCardSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _slots
            = GroupBuilder<GameScope>
                .With<ShopSlot>()
                .And<PlacedCard>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _players
            = GroupBuilder<GameScope>
                .With<PlayerActor>()
                .And<Money>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(16);

        public void Execute()
        {
            foreach (var player in _players)
            foreach (var slot in _slots.GetEntities(_buffer))
            {
                var playerMoney = player.Get<Money>().Value;

                var placedCard = slot.Get<PlacedCard>().Value.GetEntity();
                var cardPrice = placedCard.Get<Price>().Value;

                slot.Is<CanBuy>(playerMoney >= cardPrice);
            }
        }
    }
}
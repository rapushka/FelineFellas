using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class DecrementPlayerMoneyOnCardBoughtSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _slots
            = GroupBuilder<GameScope>
                .With<ShopSlot>()
                .And<PlacedCard>()
                .And<Buy>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _players
            = GroupBuilder<GameScope>
                .With<Player>()
                .And<Money>()
                .Build();

        public void Execute()
        {
            foreach (var slot in _slots)
            foreach (var player in _players)
            {
                var card = slot.Get<PlacedCard>().Value.GetEntity();
                var cardPrice = card.Get<Price>().Value;

                player.Decrement<Money>(cardPrice);
            }
        }
    }
}
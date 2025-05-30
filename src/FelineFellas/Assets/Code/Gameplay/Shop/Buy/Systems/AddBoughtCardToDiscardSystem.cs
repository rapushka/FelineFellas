using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class AddBoughtCardToDiscardSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _slots
            = GroupBuilder<GameScope>
                .With<ShopSlot>()
                .And<PlacedCard>()
                .And<Buy>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _players
            = GroupBuilder<GameScope>
                .With<Actor>()
                .And<PlayerActor>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(16);

        public void Execute()
        {
            foreach (var slot in _slots.GetEntities(_buffer))
            foreach (var player in _players)
            {
                var card = slot.Get<PlacedCard>().Value.GetEntity();
                CardUtils.Purchase(card, player.ID());
            }
        }
    }
}
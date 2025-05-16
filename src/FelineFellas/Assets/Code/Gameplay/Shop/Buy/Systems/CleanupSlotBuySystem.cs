using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CleanupSlotBuySystem : ICleanupSystem
    {
        private readonly IGroup<Entity<GameScope>> _slots
            = GroupBuilder<GameScope>
                .With<ShopSlot>()
                .And<Buy>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(4);

        public void Cleanup()
        {
            foreach (var slot in _slots.GetEntities(_buffer))
                slot.Remove<Buy>();
        }
    }
}
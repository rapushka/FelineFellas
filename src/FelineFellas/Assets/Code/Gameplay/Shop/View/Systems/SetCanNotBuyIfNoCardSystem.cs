using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class SetCanNotBuyIfNoCardSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _slots
            = GroupBuilder<GameScope>
                .With<ShopSlot>()
                .And<CanBuy>()
                .And<Empty>()
                .Build();

        public void Execute()
        {
            foreach (var slot in _slots)
                slot.Set<CanBuy, bool>(false);
        }
    }
}
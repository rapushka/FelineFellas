using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UpdateCanBuyInteractableSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _slots
            = GroupBuilder<GameScope>
                .With<ShopSlot>()
                .And<CanBuy>()
                .Build();

        public void Execute()
        {
            foreach (var slot in _slots)
            {
                slot.Is<Interactable>(slot.Get<CanBuy>().Value);
            }
        }
    }
}
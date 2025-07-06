using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class SelectFreeCellForTargetObjectSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _abilities
            = GroupBuilder<GameScope>
                .With<AbilityUse>()
                .And<TargetObjectAsFreeCell>()
                .And<TargetSubject>()
                .Build();

        public void Execute()
        {
            foreach (var ability in _abilities)
            {
                var sender = ability.Get<TargetSubject>().Value.GetEntity();
                var cell = sender.GetContainingCellID().GetEntity();

                var direction = ability.Get<TargetObjectAsFreeCell>().Value;
                var targetCell = FieldUtils.GetNeighborCell(cell, direction);

                if (targetCell is null || !targetCell.Is<Free>())
                    continue;

                ability.Set<TargetObject, EntityID>(targetCell.ID());
            }
        }
    }
}
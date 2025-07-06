using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UpdateStaminaOnCardUsedSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _abilities
            = GroupBuilder<GameScope>
                .With<AbilityUse>()
                .And<TargetSubject>()
                .Build();

        public void Execute()
        {
            foreach (var ability in _abilities)
            {
                var targetUnit = ability.Get<TargetSubject>().Value.GetEntity();
                targetUnit.Is<OutOfStamina>(true);
            }
        }
    }
}
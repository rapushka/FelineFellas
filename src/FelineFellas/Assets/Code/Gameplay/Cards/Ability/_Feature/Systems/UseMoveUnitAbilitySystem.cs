using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UseMoveUnitAbilitySystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _abilities
            = GroupBuilder<GameScope>
                .With<AbilityUse>()
                .And<TargetObject>()
                .And<AbilityMove>()
                .Build();

        public void Execute()
        {
            foreach (var ability in _abilities)
            {
                var unit = ability.Get<TargetSubject>().Value.GetEntity();
                var cell = ability.Get<TargetObject>().Value.GetEntity();

                CardUtils.PlaceCardOnField(unit, cell);
            }
        }
    }
}
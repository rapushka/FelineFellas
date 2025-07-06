using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UseSendToDiscardAbilitySystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _abilities
            = GroupBuilder<GameScope>
                .With<AbilityUse>()
                .And<TargetObject>()
                .And<AbilitySendToDiscard>()
                .Build();

        public void Execute()
        {
            foreach (var ability in _abilities)
            {
                var target = ability.Get<TargetObject>().Value.GetEntity();
                CardUtils.Discard(target);
            }
        }
    }
}
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UseAttackAbilitySystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _abilities
            = GroupBuilder<GameScope>
                .With<AbilityUse>()
                .And<AbilityAttack>()
                .And<TargetSubject>()
                .And<TargetObject>()
                .Build();

        public void Execute()
        {
            foreach (var ability in _abilities)
            {
                var multiplier = ability.Get<AbilityAttack>().Value;

                var sender = ability.Get<TargetSubject>().Value.GetEntity();
                var target = ability.Get<TargetObject>().Value.GetEntity();

                var strength = sender.Get<Strength>().Value;

                target.Decrement<Health>((int)(strength * multiplier));
            }
        }
    }
}
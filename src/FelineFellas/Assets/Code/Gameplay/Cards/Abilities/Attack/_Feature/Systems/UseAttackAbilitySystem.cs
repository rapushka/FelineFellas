using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UseAttackAbilitySystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _cards
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<Used>()
                .And<AbilityAttack>()
                .And<SelectedTarget>()
                .Build();

        public void Execute()
        {
            foreach (var card in _cards)
            {
                var multiplier = card.Get<ActionValue>().Value;

                var attacker = card.Get<UseTarget>().Value.GetEntity();
                var target = card.Get<SelectedTarget>().Value.GetEntity();

                var strength = attacker.Get<Strength>().Value;

                target.Decrement<Health>((int)(strength * multiplier));
            }
        }
    }
}
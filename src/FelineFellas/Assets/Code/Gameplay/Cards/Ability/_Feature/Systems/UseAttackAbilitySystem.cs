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
                .And<TargetSubject>()
                .Build();

        public void Execute()
        {
            foreach (var card in _cards)
            {
                var multiplier = card.Get<AbilityAttack>().Value;

                // TODO: use TargetSubject instead of DropCardOn
                // var sender = card.Get<DropCardOn>().Value.GetEntity();
                var sender = card.Get<TargetSubject>().Value.GetEntity();
                var target = card.Get<TargetObject>().Value.GetEntity();

                var strength = sender.Get<Strength>().Value;

                target.Decrement<Health>((int)(strength * multiplier));
            }
        }
    }
}
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public class EnemySelectUnitToApplyOrderCard : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _enemies
            = GroupBuilder<GameScope>
                .With<ActiveEnemyActor>()
                .And<TryPlayCard>()
                .And<CardToPlay>()
                .And<WillPlayActionCard>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _unitsOnField
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<UnitCard>()
                .And<OnField>()
                .And<EnemyCard>()
                .Without<OutOfStamina>()
                .Build();

        public void Execute()
        {
            foreach (var enemy in _enemies)
            {
                Entity<GameScope> useTarget = null;

                foreach (var unit in _unitsOnField)
                {
                    useTarget ??= unit;
                    var strength = unit.Get<Strength>().Value;

                    if (useTarget.Get<Strength>().Value < strength)
                        useTarget = unit;
                }

                var card = enemy.Get<CardToPlay>().Value.GetEntity();

                if (useTarget is null)
                {
                    card.Is<CanNotPlay>(true);
                    continue;
                }

                card
                    .Is<Dropped>(true)
                    .Is<WillBeUsed>(true)
                    .Set<DropCardOn, EntityID>(useTarget.ID())
                    ;
            }
        }
    }
}
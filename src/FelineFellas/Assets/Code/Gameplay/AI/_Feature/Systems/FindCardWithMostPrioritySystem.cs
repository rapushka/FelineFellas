using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public class FindCardWithMostPrioritySystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _enemies
            = GroupBuilder<GameScope>
                .With<EnemyActor>()
                .And<ActiveActor>()
                .And<TryPlayCard>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _cards
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<InHandIndex>()
                .And<EnemyCard>()
                .Without<CanNotPlay>()
                .Build();

        public void Execute()
        {
            foreach (var enemy in _enemies)
            {
                float? topPriority = null;
                Entity<GameScope> topPriorityCard = null;

                foreach (var card in _cards)
                {
                    var priority = card.Get<Priority>().Value;

                    topPriority ??= priority;
                    topPriorityCard ??= card;

                    if (topPriority < priority)
                    {
                        topPriority = priority;
                        topPriorityCard = card;
                    }
                }

                if (topPriorityCard is null)
                    continue;

                enemy
                    .Set<CardToPlay, EntityID>(topPriorityCard.ID())
                    .Is<WillPlayActionCard>(topPriorityCard.Is<OrderCard>())
                    ;
            }
        }
    }
}
using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public class TickDelayBeforeCardPlaySystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _turnMediator
            = GroupBuilder<GameScope>
                .With<TurnMediator>()
                .And<InEnemyTurnState>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _enemies
            = GroupBuilder<GameScope>
                .With<EnemyActor>()
                .And<DelayBeforePlayingCard>()
                .Build();

        private static ITimeService TimeService => ServiceLocator.Resolve<ITimeService>();

        private readonly List<Entity<GameScope>> _buffer = new(4);

        public void Execute()
        {
            foreach (var _ in _turnMediator)
            foreach (var enemy in _enemies.GetEntities(_buffer))
            {
                enemy.Decrement<DelayBeforePlayingCard>(TimeService.AnimationDelta);
                var delayIsUp = enemy.Get<DelayBeforePlayingCard>().Value <= 0f;

                if (delayIsUp)
                {
                    enemy
                        .Remove<DelayBeforePlayingCard>()
                        .Add<TryPlayCard>()
                        ;
                }
            }
        }
    }
}
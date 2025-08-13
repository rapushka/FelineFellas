using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public class StartDelayBeforeCardPlaySystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _turnMediator
            = GroupBuilder<GameScope>
                .With<TurnMediator>()
                .And<InEnemyTurnState>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _enemies
            = GroupBuilder<GameScope>
                .With<EnemyActor>()
                .And<ActiveActor>()
                .Without<DelayBeforePlayingCard>()
                .Without<TryPlayCard>()
                .Build();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private readonly List<Entity<GameScope>> _buffer = new(4);

        public void Execute()
        {
            foreach (var _ in _turnMediator)
            foreach (var enemy in _enemies.GetEntities(_buffer))
            {
                enemy
                    .Add<DelayBeforePlayingCard, float>(GameConfig.Turns.Timings.DelayBetweenEnemyPlayCard)
                    ;
            }
        }
    }
}
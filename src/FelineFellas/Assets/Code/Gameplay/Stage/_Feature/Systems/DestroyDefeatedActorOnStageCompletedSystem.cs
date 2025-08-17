using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class DestroyDefeatedActorOnStageCompletedSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<StageCompletedEvent>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _defeatedEnemy
            = GroupBuilder<GameScope>
                .With<EnemyActor>()
                .And<ActiveActor>()
                .Build();

        public void Execute()
        {
            foreach (var _ in _events)
            foreach (var enemyActor in _defeatedEnemy)
            {
                enemyActor.Is<Destroy>(true);
            }
        }
    }
}
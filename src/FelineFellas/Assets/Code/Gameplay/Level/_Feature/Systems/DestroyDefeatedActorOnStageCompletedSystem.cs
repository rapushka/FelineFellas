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
                .With<Leader>()
                .And<EnemyCard>()
                .And<Defeated>()
                .Build();

        public void Execute()
        {
            foreach (var _ in _events)
            foreach (var enemy in _defeatedEnemy)
            {
                // TODO:
            }
        }
    }
}
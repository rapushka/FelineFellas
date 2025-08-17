using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class ShowOtherEnemyLeadersOnStageCompletedSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<StageCompletedEvent>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _enemies
            = GroupBuilder<GameScope>
                .With<LeadOnStage>()
                .And<EnemyCard>()
                .And<Visible>()
                .Build();

        public void Execute()
        {
            foreach (var _ in _events)
            foreach (var leader in _enemies)
            {
                leader.Set<Visible, bool>(true);
            }
        }
    }
}
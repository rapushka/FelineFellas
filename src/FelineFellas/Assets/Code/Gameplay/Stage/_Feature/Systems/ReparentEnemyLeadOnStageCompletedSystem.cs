using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class ReparentEnemyLeadOnStageCompletedSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _event
            = GroupBuilder<GameScope>
                .With<StageCompletedEvent>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _defeatedEnemyLeads
            = GroupBuilder<GameScope>
                .With<NextEnemyLead>()
                .And<Defeated>()
                .And<EnemyCard>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(2);

        public void Execute()
        {
            foreach (var _ in _event)
            foreach (var lead in _defeatedEnemyLeads.GetEntities(_buffer))
            {
                var stage = StageUtils.GetStage(lead);
                CardUtils.Defeat(lead)
                    .SetParent(stage)
                    ;
            }
        }
    }
}
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class ReparentEnemyLeadToStageOnStageCompletedSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _event
            = GroupBuilder<GameScope>
                .With<StageCompletedEvent>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _defeatedEnemyLeads
            = GroupBuilder<GameScope>
                .With<NextEnemyLead>()
                .And<Defeated>()
                .Build();

        public void Execute()
        {
            foreach (var _ in _event)
            {
                foreach (var lead in _defeatedEnemyLeads)
                {
                    var stage = lead.Get<EnemyLeadOnStage>().Value.GetEntity();
                    lead.SetParent(stage);
                }
            }
        }
    }
}
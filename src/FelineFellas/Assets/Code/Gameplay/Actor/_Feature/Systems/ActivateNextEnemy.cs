using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class ActivateNextEnemy : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<StartFightEvent>()
                .Build();

        public void Execute()
        {
            foreach (var e in _events)
            {
                var enemyLead = e.Get<StartFightEvent>().Value.GetEntity();
                var stageID = enemyLead.Get<LeadOnStage>().Value;

                var actor = StageUtils.GetActorOnStage(stageID);
                var stage = StageUtils.GetStage(stageID);

                actor.Add<ActiveActor>();
                stage.Add<EnteringStage>();
            }
        }
    }
}
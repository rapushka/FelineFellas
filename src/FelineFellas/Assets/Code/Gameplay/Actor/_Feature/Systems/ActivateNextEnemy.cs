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
                var deck = enemyLead.Parent(); // TODO: MAKE BOUND BETWEEN LEAD AND ACTOR
                var actor = deck.Parent();     // TODO: MAKE BOUND BETWEEN LEAD AND ACTOR

                actor.Add<ActiveActor>();

                var stage = actor.Parent();
                stage.Add<EnteringStage>();
            }
        }
    }
}
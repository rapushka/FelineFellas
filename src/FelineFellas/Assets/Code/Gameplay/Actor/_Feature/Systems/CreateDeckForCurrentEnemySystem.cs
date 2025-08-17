using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CreateDeckForCurrentEnemySystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<StartFightEvent>()
                .Build();

        private static IDeckFactory DeckFactory => ServiceLocator.Resolve<IDeckFactory>();

        public void Execute()
        {
            foreach (var e in _events)
            {
                var enemyLead = e.Get<StartFightEvent>().Value.GetEntity();
                var enemyActor = enemyLead.Parent();

                enemyActor.AssertIs<Actor>();

                DeckFactory.CreateForEnemy(enemyActor, enemyLead);
            }
        }
    }
}
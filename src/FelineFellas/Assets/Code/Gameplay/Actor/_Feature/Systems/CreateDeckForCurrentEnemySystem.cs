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

        private static ICardFactory CardFactory => ServiceLocator.Resolve<ICardFactory>();

        public void Execute()
        {
            foreach (var e in _events)
            {
                var enemyLead = e.Get<StartFightEvent>().Value.GetEntity();
                var enemyActor = enemyLead.Parent();

                CardFactory.CreateDeckForEnemy(enemyActor, enemyLead);
            }
        }
    }
}
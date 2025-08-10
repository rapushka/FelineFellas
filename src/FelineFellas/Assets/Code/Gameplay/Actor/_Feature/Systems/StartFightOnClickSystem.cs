using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class StartFightOnClickSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<StartFightWithNextEnemyLead>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _nextEnemies
            = GroupBuilder<GameScope>
                .With<NextEnemy>()
                .Build();

        public void Execute()
        {
            foreach (var e in _events)
            foreach (var enemy in _nextEnemies)
            {
                CreateEntity.Empty()
                    .Add<StartFightEvent, EntityID>(enemy.ID())
                    ;

                e.Add<Destroy>();
            }
        }
    }
}
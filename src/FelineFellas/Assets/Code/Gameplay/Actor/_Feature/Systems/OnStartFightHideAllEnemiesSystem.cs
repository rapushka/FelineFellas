using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class OnStartFightHideAllEnemiesSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<StartFightEvent>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _otherEnemies
            = GroupBuilder<GameScope>
                .With<EnemyLeadOnMap>()
                .Without<NextEnemy>()
                .Build();

        public void Execute()
        {
            foreach (var _ in _events)
            foreach (var enemy in _otherEnemies)
            {
                enemy.Set<Visible, bool>(false);
            }
        }
    }
}
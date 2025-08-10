using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public static class MapUtils
    {
        private static readonly IGroup<Entity<GameScope>> EnemiesOnMap
            = GroupBuilder<GameScope>
                .With<EnemyLeadOnMap>()
                .Build();

        public static Entity<GameScope> GetNextEnemyLead()
        {
            var currentEnemy = EnemiesOnMap.FindWithMin(
                (enemy) =>
                {
                    var stage = enemy.Get<EnemyLeadOnMap>().Value.GetEntity();
                    var stageNumber = stage.Get<Stage>().Value;
                    return stageNumber;
                }
            );
            return currentEnemy;
        }
    }
}
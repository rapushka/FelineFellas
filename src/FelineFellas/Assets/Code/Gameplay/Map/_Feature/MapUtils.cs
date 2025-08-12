using Entitas;
using Entitas.Generic;
using GameEntity = Entitas.Generic.Entity<FelineFellas.GameScope>;

namespace FelineFellas
{
    public static class MapUtils
    {
        private static readonly IGroup<GameEntity> EnemiesOnMap
            = GroupBuilder<GameScope>
                .With<EnemyLeadOnMap>()
                .Build();

        public static GameEntity GetNextEnemyLead()
        {
            var currentEnemy = EnemiesOnMap
                .Where(EnemyIsNotDefeated)
                .FindWithMin(StageNumber);

            return currentEnemy
                .Add<NextEnemyLead>();

            bool EnemyIsNotDefeated(GameEntity enemy)
            {
                var stage = enemy.Get<EnemyLeadOnMap>().Value.GetEntity();
                return !stage.Is<CompletedStage>();
            }

            int StageNumber(GameEntity enemy)
            {
                var stage = enemy.Get<EnemyLeadOnMap>().Value.GetEntity();
                var stageNumber = stage.Get<Stage>().Value;
                return stageNumber;
            }
        }
    }
}
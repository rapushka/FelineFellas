using Entitas;
using Entitas.Generic;
using GameEntity = Entitas.Generic.Entity<FelineFellas.GameScope>;

namespace FelineFellas
{
    public static class MapUtils
    {
        private static readonly IGroup<GameEntity> EnemiesOnMap
            = GroupBuilder<GameScope>
                .With<EnemyLeadOnStage>()
                .Without<Defeated>()
                .Build();

        public static GameEntity GetNextEnemyLead()
        {
            var currentEnemy = EnemiesOnMap
                .FindWithMin(StageNumber);

            return currentEnemy
                .Add<NextEnemyLead>();

            int StageNumber(GameEntity enemy)
            {
                var stage = enemy.Get<EnemyLeadOnStage>().Value.GetEntity();
                var stageNumber = stage.Get<Stage>().Value;
                return stageNumber;
            }
        }
    }
}
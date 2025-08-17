using Entitas;
using Entitas.Generic;
using GameEntity = Entitas.Generic.Entity<FelineFellas.GameScope>;

namespace FelineFellas
{
    public static class MapUtils
    {
        private static readonly IGroup<GameEntity> EnemiesOnMap
            = GroupBuilder<GameScope>
                .With<LeadOnStage>()
                .Without<PlayerCard>()
                .Without<Defeated>()
                .Build();

        public static GameEntity GetNextEnemyLead()
        {
            var currentEnemy = EnemiesOnMap
                .FindWithMin(StageNumber);

            return currentEnemy
                    ?.Add<NextEnemyLead>()
                ;

            int StageNumber(GameEntity enemy)
            {
                var stageNumber = StageUtils.GetStageID(enemy).Number;
                return stageNumber;
            }
        }
    }
}
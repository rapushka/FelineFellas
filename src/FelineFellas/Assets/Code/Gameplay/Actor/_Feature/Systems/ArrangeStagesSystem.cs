using Entitas;
using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public sealed class ArrangeStagesSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<ArrangeStagesEvent>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _enemies
            = GroupBuilder<GameScope>
                .With<LeadOnStage>()
                .Without<PlayerCard>()
                .Build();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static MapConfig.ViewConfig Config => GameConfig.Map.View;

        public void Execute()
        {
            foreach (var e in _events)
            foreach (var enemyLead in _enemies)
            {
                var stageID = enemyLead.Get<LeadOnStage>().Value;
                var stageNumber = stageID.Number;

                enemyLead.Set<WorldPosition, Vector2>(
                    new(
                        x: stageNumber * Config.SpacingBetweenEnemies - Config.FirstEnemyX,
                        y: 0f
                    )
                );

                e.Is<Destroy>(true);
            }
        }
    }
}
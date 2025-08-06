using Entitas;
using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public sealed class ArrangeStagesOnEnemyInitializationSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _enemies
            = GroupBuilder<GameScope>
                .With<EnemyLeadOnMap>()
                .And<Initializing>()
                .Build();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static MapConfig.ViewConfig Config => GameConfig.Map.View;

        public void Execute()
        {
            foreach (var enemy in _enemies)
            {
                var stage = enemy.Get<EnemyLeadOnMap>().Value.GetEntity();
                var stageNumber = stage.Get<Stage>().Value;

                enemy.Set<WorldPosition, Vector2>(
                    new(
                        x: stageNumber * Config.SpacingBetweenEnemies - Config.FirstEnemyX,
                        y: 0f
                    )
                );
            }
        }
    }
}
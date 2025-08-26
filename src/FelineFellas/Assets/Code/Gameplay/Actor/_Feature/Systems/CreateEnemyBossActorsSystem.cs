using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public class CreateEnemyBossActorsSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _stages
            = GroupBuilder<GameScope>
                .With<Stage>()
                .And<Initializing>()
                .And<FinalStage>()
                .Without<PlayerStage>()
                .Build();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static IActorFactory ActorFactory => ServiceLocator.Resolve<IActorFactory>();

        private static IRandomService RandomService => ServiceLocator.Resolve<IRandomService>();

        public void Execute()
        {
            foreach (var stage in _stages)
            {
                var enemyLoadout = RandomService.PickRandom(GameConfig.Loadouts.EnemyBossLoadouts);
                var stageID = stage.ID();

                ActorFactory.CreateEnemyBossOnMap(enemyLoadout, stageID)
                    .Add<ChildOf, EntityID>(stageID);
            }
        }
    }
}
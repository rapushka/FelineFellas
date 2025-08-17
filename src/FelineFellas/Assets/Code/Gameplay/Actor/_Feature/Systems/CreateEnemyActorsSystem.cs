using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public class CreateEnemyActorsSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _stages
            = GroupBuilder<GameScope>
                .With<Stage>()
                .And<Initializing>()
                .Without<PlayerStage>()
                .Build();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static IActorFactory ActorFactory => ServiceLocator.Resolve<IActorFactory>();

        private static IRandomService RandomService => ServiceLocator.Resolve<IRandomService>();

        public void Execute()
        {
            foreach (var stage in _stages)
            {
                var enemyLoadout = RandomService.PickRandom(GameConfig.Loadouts.EnemyLoadouts);
                var stageID = stage.ID();

                ActorFactory.CreateEnemyOnMap(enemyLoadout, stageID)
                    .Add<ChildOf, EntityID>(stageID);
            }
        }
    }
}
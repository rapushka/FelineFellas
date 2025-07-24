using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public class CreateEnemyActorsSystem : IInitializeSystem
    {
        private readonly IGroup<Entity<GameScope>> _stages
            = GroupBuilder<GameScope>
                .With<Stage>()
                .Build();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static IActorFactory ActorFactory => ServiceLocator.Resolve<IActorFactory>();

        private static IRandomService RandomService => ServiceLocator.Resolve<IRandomService>();

        public void Initialize()
        {
            foreach (var stage in _stages)
            {
                var enemyLoadout = RandomService.PickRandom(GameConfig.Loadouts.EnemyLoadouts);
                ActorFactory.CreateEnemy(enemyLoadout)
                    .Add<ChildOf, EntityID>(stage.ID());
            }
        }
    }
}
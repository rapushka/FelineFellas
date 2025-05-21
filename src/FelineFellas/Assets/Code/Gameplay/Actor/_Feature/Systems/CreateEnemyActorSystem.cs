using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public class CreateEnemyActorSystem : IInitializeSystem
    {
        private readonly IGroup<Entity<GameScope>> _levels
            = GroupBuilder<GameScope>
                .With<Level>()
                .Build();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static IActorFactory ActorFactory => ServiceLocator.Resolve<IActorFactory>();

        private static IRandomService RandomService => ServiceLocator.Resolve<IRandomService>();

        public void Initialize()
        {
            foreach (var level in _levels)
            {
                var enemyLoadout = RandomService.PickRandom(GameConfig.Loadouts.EnemyLoadouts);
                ActorFactory.CreateEnemy(enemyLoadout)
                    .Add<ChildOf, EntityID>(level.ID());
            }
        }
    }
}
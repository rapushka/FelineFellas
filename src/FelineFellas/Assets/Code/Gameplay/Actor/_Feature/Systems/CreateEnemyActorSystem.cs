using Entitas;

namespace FelineFellas
{
    public class CreateEnemyActorSystem : IInitializeSystem
    {
        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static IActorFactory ActorFactory => ServiceLocator.Resolve<IActorFactory>();

        private static IRandomService RandomService => ServiceLocator.Resolve<IRandomService>();

        public void Initialize()
        {
            var enemyLoadout = RandomService.PickRandom(GameConfig.Loadouts.EnemyLoadouts);
            ActorFactory.CreateEnemy(enemyLoadout);
        }
    }
}
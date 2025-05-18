using Entitas;

namespace FelineFellas
{
    public class CreatePlayerActorSystem : IInitializeSystem
    {
        private static IGameConfig   GameConfig   => ServiceLocator.Resolve<IGameConfig>();
        private static IActorFactory ActorFactory => ServiceLocator.Resolve<IActorFactory>();

        public void Initialize()
        {
            ActorFactory.CreatePlayer(GameConfig.Loadouts.PlayerLoadout);
        }
    }
}
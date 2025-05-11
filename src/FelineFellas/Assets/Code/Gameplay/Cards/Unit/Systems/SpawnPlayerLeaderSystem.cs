using Entitas;

namespace FelineFellas
{
    public class SpawnPlayerLeaderSystem : IInitializeSystem
    {
        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static ICardFactory CardFactory => ServiceLocator.Resolve<ICardFactory>();

        public void Initialize()
        {
            CardFactory.CreateCardOnCoordinates(GameConfig.Cards.LeaderCardID, new(0, 3))
                .Add<Leader>()
                ;
        }
    }
}
using Entitas;

namespace FelineFellas
{
    public class SpawnEnemyUnitsSystem : IInitializeSystem
    {
        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static ICardFactory CardFactory => ServiceLocator.Resolve<ICardFactory>();

        public void Initialize()
        {
            foreach (var coordinate in GameConfig.Cards.EnemySpawnCoordinates)
            {
                CardFactory.CreateCardOnCoordinates(GameConfig.Cards.EnemyCardID, coordinate)
                    .Add<OnSide, Side>(Side.Enemy)
                    .Add<Enemy>()
                    ;
            }
        }
    }
}
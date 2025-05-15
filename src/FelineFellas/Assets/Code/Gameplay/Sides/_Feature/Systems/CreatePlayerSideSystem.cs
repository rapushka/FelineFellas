using Entitas;

namespace FelineFellas
{
    public class CreatePlayerSideSystem : IInitializeSystem
    {
        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        public void Initialize()
        {
            CreateEntity.Empty()
                .Add<Player>()
                .Add<Money, int>(GameConfig.MoneyConfig.MoneyOnStart)
                ;
        }
    }
}
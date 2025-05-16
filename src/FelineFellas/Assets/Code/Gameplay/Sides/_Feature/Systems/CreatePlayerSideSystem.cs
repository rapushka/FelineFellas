using Entitas;

namespace FelineFellas
{
    public class CreatePlayerSideSystem : IInitializeSystem
    {
        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        public void Initialize()
        {
            CreateEntity.Empty()
                .Add<Name, string>("player")
                .Add<Player>()
                .Add<Money, int>(GameConfig.Money.MoneyOnStart)
                ;
        }
    }
}
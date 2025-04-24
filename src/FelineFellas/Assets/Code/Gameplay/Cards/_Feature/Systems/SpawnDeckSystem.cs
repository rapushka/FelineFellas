using Entitas;

namespace FelineFellas
{
    public sealed class SpawnDeckSystem : IInitializeSystem
    {
        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static ICardFactory CardFactory => ServiceLocator.Resolve<ICardFactory>();

        public void Initialize()
        {
            foreach (var cardID in GameConfig.Cards.StartPlayerDeck)
                CardFactory.CreateInDeck(cardID);
        }
    }
}
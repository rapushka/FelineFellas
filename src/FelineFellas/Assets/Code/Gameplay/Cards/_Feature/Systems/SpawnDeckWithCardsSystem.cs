using Entitas;

namespace FelineFellas
{
    public sealed class SpawnDeckWithCardsSystem : IInitializeSystem
    {
        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static ICardFactory CardFactory => ServiceLocator.Resolve<ICardFactory>();

        public void Initialize()
        {
            var deck = CardFactory.CreateDeck();

            foreach (var (cardID, count) in GameConfig.Cards.PlayerDeckOnStart)
            {
                for (var i = 0; i < count; i++)
                    CardFactory.CreateCardInDeck(cardID, deck);
            }
        }
    }
}
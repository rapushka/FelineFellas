using Entitas.Generic;

namespace FelineFellas
{
    public interface ICardFactory : IService
    {
        Entity<GameScope> CreateInDeck(CardIDRef cardID);
    }

    public class CardFactory : ICardFactory
    {
        private static IViewFactory ViewFactory => ServiceLocator.Resolve<IViewFactory>();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static CardsConfig Config => GameConfig.Cards;

        public Entity<GameScope> CreateInDeck(CardIDRef cardID)
        {
            return ViewFactory.CreateInWorld(Config.View.ViewPrefab, Config.View.DeckSpawnPosition).Entity
                    .Add<Card>()
                    .Add<Interactable>()
                    .Add<SpriteSortingGroup, SortGroup>(SortGroup.CardInHand)
                    .Add<InDeck>()
                    .Add<MovementSpeed, float>(Config.View.CardAnimationsSpeed)
                ;
        }
    }
}
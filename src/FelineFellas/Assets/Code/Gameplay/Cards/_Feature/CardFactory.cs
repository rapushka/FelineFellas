using Entitas.Generic;

namespace FelineFellas
{
    public interface ICardFactory : IService
    {
        Entity<GameScope> Create(CardIDRef cardID);
    }

    public class CardFactory : ICardFactory
    {
        private static IViewFactory ViewFactory => ServiceLocator.Resolve<IViewFactory>();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static CardsConfig Config => GameConfig.Cards;

        public Entity<GameScope> Create(CardIDRef cardID)
        {
            return ViewFactory.CreateInWorld(Config.View.ViewPrefab, Config.View.DeckSpawnPosition).Entity
                    .Add<Interactable>()
                    .Add<Sorting, SortGroup>(SortGroup.CardInHand)
                ;
        }
    }
}
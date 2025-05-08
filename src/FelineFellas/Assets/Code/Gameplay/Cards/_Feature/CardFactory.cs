using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public interface ICardFactory : IService
    {
        Entity<GameScope> CreateDeck();

        Entity<GameScope> CreateCardInDeck(CardIDRef cardID, Entity<GameScope> deck);
    }

    public class CardFactory : ICardFactory
    {
        private static IViewFactory ViewFactory => ServiceLocator.Resolve<IViewFactory>();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static CardsConfig CardsConfig => GameConfig.Cards;

        public Entity<GameScope> CreateDeck()
            => CreateEntity.Empty()
                .Add<Deck>()
                .Add<WorldPosition, Vector2>(CardsConfig.View.DeckSpawnPosition);

        public Entity<GameScope> CreateCardInDeck(CardIDRef cardID, Entity<GameScope> deck)
        {
            var config = CardsConfig.GetConfig(cardID);

            var isGlobal = config.Usage is CardConfig.UsageType.Global;
            var isUnit = config.Usage is CardConfig.UsageType.Unit;

            var card = ViewFactory.CreateInWorld(CardsConfig.View.ViewPrefab, deck.WorldPosition()).Entity
                .Add<Card, CardIDRef>(config.ID)
                .Add<SpriteSortingGroup, SortGroup>(SortGroup.CardInHand)
                .Add<AnimationsSpeed, float>(CardsConfig.View.CardAnimationsSpeed)
                .Add<Rotation, float>(0f)
                .Add<Scale, float>(1f)
                .Is<GlobalCard>(isGlobal)
                .Is<UnitCard>(isUnit)
                .Is<OneShotCard>(isGlobal)
                .Add<CardTitle, string>(config.Title)
                .Add<CardIcon, Sprite>(config.Icon);

            return CardUtils.AddToDeck(card, deck);
        }
    }
}
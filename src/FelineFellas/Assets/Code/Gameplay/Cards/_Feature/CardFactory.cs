using Entitas.Generic;
using UnityEngine;

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

        private static CardsConfig CardsConfig => GameConfig.Cards;

        public Entity<GameScope> CreateInDeck(CardIDRef cardID)
        {
            var config = CardsConfig.GetConfig(cardID);

            var isGlobal = config.Usage is CardConfig.UsageType.Global;
            var isUnit = config.Usage is CardConfig.UsageType.Unit;

            return ViewFactory.CreateInWorld(CardsConfig.View.ViewPrefab, CardsConfig.View.DeckSpawnPosition).Entity
                    .Add<Card, CardIDRef>(config.ID)
                    .Add<Interactable>()
                    .Add<SpriteSortingGroup, SortGroup>(SortGroup.CardInHand)
                    .Add<InDeck>()
                    .Add<AnimationsSpeed, float>(CardsConfig.View.CardAnimationsSpeed)
                    .Add<Rotation, float>(0f)
                    .Add<Scale, float>(1f)
                    .Add<Draggable>()
                    .Is<GlobalCard>(isGlobal)
                    .Is<UnitCard>(isUnit)
                    .Is<OneShotCard>(isGlobal)
                    .Add<CardTitle, string>(config.Title)
                    .Add<CardIcon, Sprite>(config.Icon)
                ;
        }
    }
}
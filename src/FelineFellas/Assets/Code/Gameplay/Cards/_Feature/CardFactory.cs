using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public interface ICardFactory : IService
    {
        Entity<GameScope> CreateDeck();

        Entity<GameScope> CreateCardInDeck(CardIDRef cardID, Entity<GameScope> deck);
        Entity<GameScope> CreateCardOnCoordinates(CardIDRef cardID, Coordinates coordinates);
    }

    public class CardFactory : ICardFactory
    {
        private static IViewFactory ViewFactory => ServiceLocator.Resolve<IViewFactory>();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static CardsConfig CardsConfig => GameConfig.Cards;

        private static PrimaryEntityIndex<GameScope, CellCoordinates, Coordinates> CellIndex
            => Contexts.Instance.Get<GameScope>().GetPrimaryIndex<CellCoordinates, Coordinates>();

        public Entity<GameScope> CreateDeck()
            => CreateEntity.Empty()
                .Add<Deck>()
                .Add<WorldPosition, Vector2>(CardsConfig.View.DeckSpawnPosition);

        public Entity<GameScope> CreateCardInDeck(CardIDRef cardID, Entity<GameScope> deck)
        {
            return Create(cardID, deck.WorldPosition())
                .Chain(card => CardUtils.AddToDeck(card, deck));
        }

        public Entity<GameScope> CreateCardOnCoordinates(CardIDRef cardID, Coordinates coordinates)
        {
            var cell = CellIndex.GetEntity(coordinates);

            return Create(cardID, cell.WorldPosition())
                .Chain(card => CardUtils.PlaceCardOnGrid(card, coordinates));
        }

        private Entity<GameScope> Create(CardIDRef cardID, Vector2 position)
        {
            var config = CardsConfig.GetConfig(cardID);

            var isGlobal = config.Usage is CardConfig.UsageType.Global;
            var isUnit = config.Usage is CardConfig.UsageType.Unit;
            var isAction = config.Usage is CardConfig.UsageType.Action;

            var card = ViewFactory.CreateInWorld(CardsConfig.View.ViewPrefab, position).Entity
                .Add<Card, CardIDRef>(config.ID)
                .Add<SpriteSortingGroup, SortGroup>(SortGroup.CardInHand)
                .Add<AnimationsSpeed, float>(CardsConfig.View.CardAnimationsSpeed)
                .Add<Rotation, float>(0f)
                .Add<Scale, float>(1f)
                .Is<GlobalCard>(isGlobal)
                .Is<UnitCard>(isUnit)
                .Is<ActionCard>(isAction)
                .Is<OneShotCard>(isGlobal || isAction)
                .Add<CardTitle, string>(config.Title)
                .Add<CardIcon, Sprite>(config.Icon);

            if (isAction)
                SetupActionCard(card, config);

            if (isUnit)
                SetupUnitCard(card, config);

            return card;
        }

        private void SetupUnitCard(Entity<GameScope> card, CardConfig config)
        {
            var unitConfig = config.UnitCardConfig;

            card
                .Add<MaxHealth, float>(unitConfig.MaxHealth)
                .Add<Health, float>(unitConfig.MaxHealth)
                ;
        }

        private void SetupActionCard(Entity<GameScope> card, CardConfig config)
        {
            var actionCardConfig = config.ActionCardConfig;
            var actionValue = actionCardConfig.Value;

            var isMove = actionCardConfig.ActionType is ActionCardConfig.ActionCardType.Move;
            var isAttack = actionCardConfig.ActionType is ActionCardConfig.ActionCardType.Attack;

            var selectTargetAsDirection = actionCardConfig.TargetSelection is ActionCardConfig.TargetSelectionType.Direction;
            var targetClosestOpponent = actionCardConfig.TargetSelection is ActionCardConfig.TargetSelectionType.ClosestOpponent;

            card
                .Add<ActionValue, float>(actionValue)
                .Is<AbilityMove>(isMove)
                .Is<AbilityAttack>(isAttack)
                .Is<OnlyForAllies>(actionCardConfig.OnlyForAllies)
                .Is<TargetSelectClosestOpponent>(targetClosestOpponent)
                ;

            if (selectTargetAsDirection)
            {
                var direction = actionCardConfig.Direction.ToCoordinates();
                card.Add<TargetSelectNeighbor, Coordinates>(direction.Multiply((int)actionValue));
            }
        }
    }
}
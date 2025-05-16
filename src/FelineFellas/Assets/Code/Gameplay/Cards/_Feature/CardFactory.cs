using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public interface ICardFactory : IService
    {
        Entity<GameScope> CreateDeck();

        Entity<GameScope> CreateCardInDeck(CardIDRef cardID, Entity<GameScope> deck);
        Entity<GameScope> CreateCardOnCoordinates(CardIDRef cardID, Coordinates coordinates);

        Entity<GameScope> CreateCardInShop(CardIDRef cardID, Entity<GameScope> shopSlot);
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
                .Add<Name, string>("deck")
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

        public Entity<GameScope> CreateCardInShop(CardIDRef cardID, Entity<GameScope> shopSlot)
            => Create(cardID, shopSlot.WorldPosition().Add(x: 2f))
                .Chain(card => CardUtils.PlaceCardInShop(card, shopSlot));

        private Entity<GameScope> Create(CardIDRef cardID, Vector2 position)
        {
            var config = CardsConfig.GetConfig(cardID);

            var isGlobal = config.Usage is CardConfig.UsageType.Global;
            var isUnit = config.Usage is CardConfig.UsageType.Unit;
            var isAction = config.Usage is CardConfig.UsageType.Action;

            var view = ViewFactory.CreateInWorld(CardsConfig.View.ViewPrefab, position);

            var card = view.Entity
                    .Add<Name, string>("card")
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
                    .Add<CardIcon, Sprite>(config.Icon)
                    .Add<Price, int>(config.Price)
                ;

            if (isAction)
                SetupActionCard(card, config);

            if (isUnit)
                SetupUnitCard(card, config);

            var viewMediator = view.GetComponent<CardViewMediator>();
            viewMediator.Initialize(card);

            return card;
        }

        private void SetupUnitCard(Entity<GameScope> card, CardConfig config)
        {
            var unitConfig = config.UnitCardConfig;

            card
                .Add<MaxHealth, int>((int)unitConfig.MaxHealth)
                .Add<Health, int>((int)unitConfig.MaxHealth)
                .Add<Strength, int>((int)unitConfig.Strength)
                ;
        }

        private void SetupActionCard(Entity<GameScope> card, CardConfig config)
        {
            var actionCardConfig = config.ActionCardConfig;
            var actionValue = actionCardConfig.Value;

            var isMove = actionCardConfig.ActionType is ActionCardConfig.ActionCardType.Move;
            var isAttack = actionCardConfig.ActionType is ActionCardConfig.ActionCardType.Attack;
            var isSendToDiscard = actionCardConfig.ActionType is ActionCardConfig.ActionCardType.SendToDiscard;

            var selectTargetAsDirection = actionCardConfig.TargetSelection is ActionCardConfig.TargetSelectionType.Direction;
            var targetClosestOpponent = actionCardConfig.TargetSelection is ActionCardConfig.TargetSelectionType.ClosestOpponent;

            var canUseOnFella = actionCardConfig.AllowedTargets.HasFlag(ActionCardConfig.AllowedTargetType.Fella);
            var canUseOnLead = actionCardConfig.AllowedTargets.HasFlag(ActionCardConfig.AllowedTargetType.Lead);
            var canUseOnEnemy = actionCardConfig.AllowedTargets.HasFlag(ActionCardConfig.AllowedTargetType.Enemy);

            card
                .Add<ActionValue, float>(actionValue)
                .Is<AbilityMove>(isMove)
                .Is<AbilityAttack>(isAttack)
                .Is<AbilitySendToDiscard>(isSendToDiscard)
                .Is<CanUseOnFella>(canUseOnFella)
                .Is<CanUseOnLeader>(canUseOnLead)
                .Is<CanUseOnEnemy>(canUseOnEnemy)
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
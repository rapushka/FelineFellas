using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public interface ICardFactory : IService
    {
        Entity<GameScope> CreateDeckWithCards(CardEntry[] cards, Side side);

        Entity<GameScope> CreateLeadOnDeck(CardIDRef cardID, Entity<GameScope> deck);

        Entity<GameScope> CreateCardInShop(CardIDRef cardID, Entity<GameScope> shopSlot);
    }

    public class CardFactory : ICardFactory
    {
        private static IViewFactory ViewFactory => ServiceLocator.Resolve<IViewFactory>();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static CardsConfig CardsConfig => GameConfig.Cards;

        public Entity<GameScope> CreateDeckWithCards(CardEntry[] cards, Side side)
        {
            var position = side.Visit(
                onPlayer: () => GameConfig.Layout.PlayerDeck,
                onEnemy: () => GameConfig.Layout.EnemyDeck
            );

            var rotation = side.Visit(
                onPlayer: () => 0f,
                onEnemy: () => 180f
            );

            var deck = CreateEntity.Empty()
                    .Add<Name, string>("deck")
                    .Add<Deck>()
                    .Add<WorldPosition, Vector2>(position)
                    .Add<OnSide, Side>(side)
                    .Add<Rotation, float>(rotation)
                ;

            foreach (var (cardID, count) in cards)
            {
                for (var i = 0; i < count; i++)
                {
                    Create(cardID, deck.WorldPosition())
                        .AssignToSide(side)
                        .Chain(c => CardUtils.AddToDeck(c, deck))
                        .Set<Rotation, float>(deck.Get<Rotation, float>())
                        ;
                }
            }

            return deck;
        }

        public Entity<GameScope> CreateLeadOnDeck(CardIDRef cardID, Entity<GameScope> deck)
        {
            var side = deck.Get<OnSide>().Value;

            return Create(cardID, deck.WorldPosition())
                    .AssignToSide(side)
                    .Chain(card => CardUtils.AddToDeck(card, deck))
                    .Set<CardFace, Face>(Face.FaceUp)
                    .Remove<CardInDeck>()
                    .Add<LeadOnDeck, EntityID>(deck.ID())
                    .SetSorting(RenderOrder.LeadOnDeck)
                ;
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
                    .Add<SpriteSortingGroup, RenderOrder>(RenderOrder.CardInHand)
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
                    .Set<CardFace, Face>(Face.FaceDown)
                    .Add<Priority, float>(config.EnemyAi.Priority)
                    .Is<CanUseOnlyOnOurRow>(config.CanUseOnlyOnOurRow)
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
                .Add<MaxHealth, int>(unitConfig.MaxHealth)
                .Add<Health, int>(unitConfig.MaxHealth)
                .Add<Strength, int>(unitConfig.Strength)
                .Is<Leader>(unitConfig.IsLeader)
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
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

        private static IAbilityFactory AbilityFactory => ServiceLocator.Resolve<IAbilityFactory>();

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

            var rotation = side.Visit(
                onPlayer: () => 0f,
                onEnemy: () => 180f
            );

            return Create(cardID, deck.WorldPosition())
                    .AssignToSide(side)
                    .Chain(card => CardUtils.AddToDeck(card, deck))
                    .Set<CardFace, Face>(Face.FaceUp)
                    .Remove<CardInDeck>()
                    .Add<LayingOnDeck, EntityID>(deck.ID())
                    .SetSorting(RenderOrder.LeadOnDeck)
                    .Set<Rotation, float>(rotation)
                ;
        }

        public Entity<GameScope> CreateCardInShop(CardIDRef cardID, Entity<GameScope> shopSlot)
            => Create(cardID, shopSlot.WorldPosition().Add(x: 2f))
                .Chain(card => CardUtils.PlaceCardInShop(card, shopSlot));

        private Entity<GameScope> Create(CardIDRef cardID, Vector2 position)
        {
            var config = CardsConfig.GetConfig(cardID);

            var isGlobal = config.Card is CardConfig.CardType.Event;
            var isUnit = config.Card is CardConfig.CardType.Unit;
            var isAction = config.Card is CardConfig.CardType.Order;

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
                    .Is<OrderCard>(isAction)
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
            var orderConfig = config.OrderCardConfig;
            var targetSubject = orderConfig.TargetSubject;
            var canUseOnFella = targetSubject.HasFlag(OrderCardConfig.AllowedTargetSubjectType.Fella);
            var canUseOnLead = targetSubject.HasFlag(OrderCardConfig.AllowedTargetSubjectType.Lead);
            var canUseOnEnemy = targetSubject.HasFlag(OrderCardConfig.AllowedTargetSubjectType.Enemy);

            AbilityFactory.Create(card.ID(), orderConfig.Ability)
                .SetParent(card)
                .Is<CanTargetSubjectFella>(canUseOnFella)
                .Is<CanTargetSubjectLeader>(canUseOnLead)
                .Is<CanTargetSubjectEnemy>(canUseOnEnemy)
                .Add<TriggerOnUse>()
                ;
        }
    }
}
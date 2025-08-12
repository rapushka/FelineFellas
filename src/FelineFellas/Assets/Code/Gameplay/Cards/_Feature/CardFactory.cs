using UnityEngine;
using GameEntity = Entitas.Generic.Entity<FelineFellas.GameScope>;

namespace FelineFellas
{
    public interface ICardFactory : IService
    {
        GameEntity CreateDeckWithCards(CardEntry[] cards, Side side);

        GameEntity CreateLeadOnDeck(CardIDRef cardID, GameEntity deck);

        GameEntity CreateEnemyLeadOnMap(CardIDRef cardID, EntityID stageID);

        GameEntity CreateDeckForEnemy(GameEntity enemyActor, GameEntity enemyLead);

        GameEntity CreateCardInShop(CardIDRef cardID, GameEntity shopSlot);
    }

    public class CardFactory : ICardFactory
    {
        private static IViewFactory ViewFactory => ServiceLocator.Resolve<IViewFactory>();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static IAbilityFactory AbilityFactory => ServiceLocator.Resolve<IAbilityFactory>();

        private static CardsConfig CardsConfig => GameConfig.Cards;

        private static IActorFactory ActorFactory => ServiceLocator.Resolve<IActorFactory>();

        public GameEntity CreateDeckWithCards(CardEntry[] cards, Side side)
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

        public GameEntity CreateLeadOnDeck(CardIDRef cardID, GameEntity deck)
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

        public GameEntity CreateEnemyLeadOnMap(CardIDRef cardID, EntityID stageID)
        {
            return Create(cardID, new())
                    .AssignToSide(Side.Enemy)
                    // .Chain(card => CardUtils.AddToDeck(card, deck))
                    .Set<CardFace, Face>(Face.FaceUp)
                    .RemoveSafely<CardInDeck>()
                    // .Add<LayingOnDeck, EntityID>(deck.ID())
                    .SetSorting(RenderOrder.LeadOnDeck)
                    .Set<Rotation, float>(0f)
                    .Add<EnemyLeadOnMap, EntityID>(stageID)
                ;
        }

        public GameEntity CreateDeckForEnemy(GameEntity enemyActor, GameEntity enemyLead)
        {
            var loadout = enemyActor.Get<EnemyLoadout>().Value;
            enemyActor
                .Chain(a => ActorFactory.CreateDeck(a, loadout))
                ;

            var deckID = enemyActor.Get<OwnedDeck>().Value;

            return enemyLead
                .Chain(card => CardUtils.AddToDeck(card, deckID.GetEntity()))
                .Add<LayingOnDeck, EntityID>(deckID);
        }

        public GameEntity CreateCardInShop(CardIDRef cardID, GameEntity shopSlot)
            => Create(cardID, shopSlot.WorldPosition().Add(x: 2f))
                .Chain(card => CardUtils.PlaceCardInShop(card, shopSlot));

        private GameEntity Create(CardIDRef cardID, Vector2 position)
        {
            var config = CardsConfig.GetConfig(cardID);

            var isEvent = config.Card is CardConfig.CardType.Event;
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
                    .Is<EventCard>(isEvent)
                    .Is<UnitCard>(isUnit)
                    .Is<OrderCard>(isAction)
                    .Is<DiscardAfterUse>(isEvent || isAction)
                    .Add<CardTitle, string>(config.Title)
                    .Add<CardIcon, Sprite>(config.Icon)
                    .Add<Price, int>(config.Price)
                    .Set<CardFace, Face>(Face.FaceDown)
                    .Add<Priority, float>(config.EnemyAi.Priority)
                    .Is<CanUseOnlyOnOurRow>(config.CanUseOnlyOnOurRow)
                    .Chain(e => SetupEventCard(e, config), @if: isEvent)
                    .Chain(e => SetupActionCard(e, config), @if: isAction)
                    .Chain(e => SetupUnitCard(e, config), @if: isUnit)
                    .Is<Initializing>(true)
                    .Add<Visible, bool>(true)
                ;

            var viewMediator = view.GetComponent<CardViewMediator>();
            viewMediator.Initialize(card);

            return card;
        }

        private GameEntity SetupEventCard(GameEntity card, CardConfig config)
        {
            var eventConfig = config.EventCardConfig;
            CreateAbilityForCard(card, eventConfig.Ability)
                ;

            card
                .Is<TargetGlobal>(eventConfig.IsGlobal)
                ;

            return card;
        }

        private GameEntity SetupActionCard(GameEntity card, CardConfig config)
        {
            var orderConfig = config.OrderCardConfig;
            var targetSubject = orderConfig.TargetSubject;

            var canUseOnFella = targetSubject.HasFlag(OrderCardConfig.AllowedTargetSubjectType.Fella);
            var canUseOnLead = targetSubject.HasFlag(OrderCardConfig.AllowedTargetSubjectType.Lead);
            var canUseOnEnemy = targetSubject.HasFlag(OrderCardConfig.AllowedTargetSubjectType.Enemy);

            card
                .Is<CanTargetSubjectFella>(canUseOnFella) // TODO: does Ability need these?
                .Is<CanTargetSubjectLeader>(canUseOnLead)
                .Is<CanTargetSubjectEnemy>(canUseOnEnemy)
                ;

            CreateAbilityForCard(card, orderConfig.Ability);

            return card;
        }

        private GameEntity SetupUnitCard(GameEntity card, CardConfig config)
        {
            var unitConfig = config.UnitCardConfig;

            return card
                    .Add<MaxHealth, int>(unitConfig.MaxHealth)
                    .Add<Health, int>(unitConfig.MaxHealth)
                    .Add<Strength, int>(unitConfig.Strength)
                    .Is<Leader>(unitConfig.IsLeader)
                ;
        }

        // ReSharper disable once UnusedMethodReturnValue.Local - why so greedy?
        private GameEntity CreateAbilityForCard(GameEntity card, AbilityConfig abilityConfig)
        {
            return AbilityFactory.Create(card.ID(), abilityConfig)
                    .SetParent(card)
                    .Add<TriggerOnUse>()
                ;
        }
    }
}
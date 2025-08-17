using UnityEngine;
using GameEntity = Entitas.Generic.Entity<FelineFellas.GameScope>;

namespace FelineFellas
{
    public interface IDeckFactory : IService
    {
        GameEntity CreateForEnemy(GameEntity enemyActor, GameEntity enemyLead);

        GameEntity Create(GameEntity actor, LoadoutConfig loadout);
    }

    public class DeckFactory : IDeckFactory
    {
        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static ICardFactory CardFactory => ServiceLocator.Resolve<ICardFactory>();

        public GameEntity CreateForEnemy(GameEntity enemyActor, GameEntity enemyLead)
        {
            var loadout = enemyActor.Get<EnemyLoadout>().Value;
            enemyActor
                .Chain(a => Create(a, loadout))
                ;

            var deckID = enemyActor.Get<OwnedDeck>().Value;

            return enemyLead
                .Chain(card => CardUtils.AddToDeck(card, deckID.GetEntity()))
                .Add<LayingOnDeck, EntityID>(deckID);
        }

        public GameEntity Create(GameEntity actor, LoadoutConfig loadout)
        {
            var side = actor.Get<OnSide>().Value;
            var deckID = CreateDeckWithCards(loadout.Deck, side)
                .Add<ChildOf, EntityID>(actor.ID())
                .ID();

            actor.Add<OwnedDeck, EntityID>(deckID);

            return actor;
        }

        private GameEntity CreateDeckWithCards(CardEntry[] cards, Side side)
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
                    CardFactory.Create(cardID, deck.WorldPosition())
                        .AssignToSide(side)
                        .Chain(c => CardUtils.AddToDeck(c, deck))
                        .Set<Rotation, float>(deck.Get<Rotation, float>())
                        ;
                }
            }

            return deck;
        }
    }
}
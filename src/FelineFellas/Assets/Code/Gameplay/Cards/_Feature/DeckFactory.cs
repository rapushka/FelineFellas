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

            var deck = enemyActor.GetOwnedDeck();

            return enemyLead
                .Chain(card => CardUtils.SendToDeck(card, deck))
                .Add<LayingOnDeck, EntityID>(deck.ID());
        }

        public GameEntity Create(GameEntity actor, LoadoutConfig loadout)
        {
            CreateDeckWithCards(loadout.Deck, actor)
                .Add<ChildOf, EntityID>(actor.ID())
                ;

            return actor;
        }

        private GameEntity CreateDeckWithCards(CardEntry[] cards, GameEntity actor)
        {
            var stageID = StageUtils.GetStageID(actor);
            var side = actor.Get<OnSide>().Value;

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
                    .Add<DeckOnStage, StageID>(stageID)
                ;

            foreach (var (cardID, count) in cards)
            {
                for (var i = 0; i < count; i++)
                {
                    CardFactory.Create(cardID, deck.WorldPosition())
                        .AssignToSide(side)
                        .Chain(c => CardUtils.SendToDeck(c, deck))
                        .Set<Rotation, float>(deck.Get<Rotation, float>())
                        .Add<CardOnStage, StageID>(stageID)
                        ;
                }
            }

            return deck;
        }
    }
}
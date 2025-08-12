using GameEntity = Entitas.Generic.Entity<FelineFellas.GameScope>;

namespace FelineFellas
{
    public interface IActorFactory : IService
    {
        GameEntity CreatePlayer(LoadoutConfig loadout);
        GameEntity CreateEnemyOnMap(LoadoutConfig loadout, EntityID stageID);

        GameEntity CreateDeck(GameEntity actor, LoadoutConfig loadout);
    }

    public class ActorFactory : IActorFactory
    {
        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static ICardFactory CardFactory => ServiceLocator.Resolve<ICardFactory>();

        public GameEntity CreatePlayer(LoadoutConfig loadout)
        {
            var actor = Create(loadout, Side.Player)
                    .Add<Name, string>("player")
                    .Add<PlayerActor>()
                    .Add<Money, int>(GameConfig.Money.MoneyOnStart)
                    .Chain(a => CreateDeck(a, loadout))
                    .Chain(a => CreateLeadOnDeck(a, loadout))
                ;

            return actor;
        }

        public GameEntity CreateEnemyOnMap(LoadoutConfig loadout, EntityID stageID)
        {
            var actor = Create(loadout, Side.Enemy)
                    .Add<Name, string>("enemy")
                    .Add<EnemyActorOnStage, EntityID>(stageID)
                    .Chain(a => CreateLeadOnMap(a, loadout))
                    .Add<EnemyLoadout, LoadoutConfig>(loadout)
                ;

            return actor;
        }

        private GameEntity Create(LoadoutConfig loadout, Side side)
        {
            var actor = CreateEntity.Empty()
                    .Add<Actor>()
                    .Add<HandSize, int>(loadout.HandSize)
                    .Add<OnSide, Side>(side)
                ;

            return actor;
        }

        public GameEntity CreateDeck(GameEntity actor, LoadoutConfig loadout)
        {
            var side = actor.Get<OnSide>().Value;
            var deckID = CardFactory.CreateDeckWithCards(loadout.Deck, side)
                .Add<ChildOf, EntityID>(actor.ID())
                .ID();

            actor.Add<OwnedDeck, EntityID>(deckID);

            return actor;
        }

        private GameEntity CreateLeadOnDeck(GameEntity actor, LoadoutConfig loadout)
        {
            var deck = actor.Get<OwnedDeck>().Value.GetEntity();
            CardFactory.CreateLeadOnDeck(loadout.Lead, deck);

            return actor;
        }

        private GameEntity CreateLeadOnMap(GameEntity actor, LoadoutConfig loadout)
        {
            var stage = actor.Get<EnemyActorOnStage>().Value;
            CardFactory.CreateEnemyLeadOnMap(loadout.Lead, stage)
                .SetParent(actor)
                ;

            return actor;
        }
    }
}
using Entitas.Generic;

namespace FelineFellas
{
    public interface IActorFactory : IService
    {
        Entity<GameScope> CreatePlayer(LoadoutConfig loadout);
        Entity<GameScope> CreateEnemy(LoadoutConfig loadout);
    }

    public class ActorFactory : IActorFactory
    {
        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static ICardFactory CardFactory => ServiceLocator.Resolve<ICardFactory>();

        public Entity<GameScope> CreatePlayer(LoadoutConfig loadout)
        {
            var actor = Create(loadout, Side.Player)
                    .Add<Name, string>("player")
                    .Add<PlayerActor>()
                    .Add<Money, int>(GameConfig.Money.MoneyOnStart)
                ;

            return actor;
        }

        public Entity<GameScope> CreateEnemy(LoadoutConfig loadout)
        {
            var actor = Create(loadout, Side.Enemy)
                    .Add<Name, string>("enemy")
                    .Add<EnemyActor>()
                ;

            return actor;
        }

        private Entity<GameScope> Create(LoadoutConfig loadout, Side side)
        {
            var actor = CreateEntity.Empty()
                    .Add<Actor>()
                    .Add<HandSize, int>(loadout.HandSize)
                    .Add<OnSide, Side>(side)
                    .Chain(a => CreateDeck(a, loadout))
                    .Chain(a => CreateCardsOnField(a, loadout))
                ;

            return actor;
        }

        private Entity<GameScope> CreateDeck(Entity<GameScope> actor, LoadoutConfig loadout)
        {
            var side = actor.Get<OnSide>().Value;
            var deckID = CardFactory.CreateDeckWithCards(loadout.Deck, side)
                .ID();

            actor.Add<OwnedDeck, EntityID>(deckID);

            return actor;
        }

        private Entity<GameScope> CreateCardsOnField(Entity<GameScope> actor, LoadoutConfig loadout)
        {
            var side = actor.Get<OnSide>().Value;

            foreach (var (id, coordinates) in loadout.UnitsOnField)
                CardFactory.CreateCardOnCoordinates(id, coordinates, side);

            return actor;
        }
    }
}
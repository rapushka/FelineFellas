using System.Linq;
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
                    .Add<Player>()
                    .Add<Money, int>(GameConfig.Money.MoneyOnStart)
                ;

            var deckID = actor.Get<OwnedDeck>().Value;
            foreach (var unit in DeckUtils.GetAllCardsInDeck(deckID).Where(c => c.Is<UnitCard>()))
                unit.Is<Fella>(!unit.Is<Leader>());

            return actor;
        }

        public Entity<GameScope> CreateEnemy(LoadoutConfig loadout)
        {
            var actor = CreateEntity.Empty() // TODO: use common Create
                    .Add<Actor>()
                    .Add<HandSize, int>(loadout.HandSize)
                    .Add<OnSide, Side>(Side.Enemy)
                    // .Chain(a => CreateDeck(a, loadout)) TODO:
                    .Chain(a => CreateCardsOnField(a, loadout))
                    .Add<Name, string>("enemy")
                    .Add<Enemy>()
                ;

            // var deckID = actor.Get<OwnedDeck>().Value;
            // foreach (var unit in DeckUtils.GetAllCardsInDeck(deckID).Where(c => c.Is<UnitCard>()))
            //     unit.Is<EnemyUnit>(true);

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
            var deckID = CardFactory.CreateDeckWithCards(loadout.Deck)
                .Add<OnSide, Side>(side)
                .ID();

            actor.Add<OwnedDeck, EntityID>(deckID);

            foreach (var card in DeckUtils.GetAllCardsInDeck(deckID))
                card.Add<OnSide, Side>(side);

            return actor;
        }

        private Entity<GameScope> CreateCardsOnField(Entity<GameScope> actor, LoadoutConfig loadout)
        {
            var side = actor.Get<OnSide>().Value;

            foreach (var (id, coordinates) in loadout.UnitsOnField)
            {
                CardFactory.CreateCardOnCoordinates(id, coordinates)
                    .Add<OnSide, Side>(side)
                    .AddSideFlag();
            }

            return actor;
        }
    }
}
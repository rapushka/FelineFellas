using Entitas.Generic;

namespace FelineFellas
{
    public interface IActorFactory : IService
    {
        Entity<GameScope> CreatePlayer(LoadoutConfig loadout);
    }

    public class ActorFactory : IActorFactory
    {
        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static ICardFactory CardFactory => ServiceLocator.Resolve<ICardFactory>();

        public Entity<GameScope> CreatePlayer(LoadoutConfig loadout)
        {
            var actor = Create(loadout)
                    .Add<Name, string>("player")
                    .Add<Player>()
                    .Add<Money, int>(GameConfig.Money.MoneyOnStart)
                ;

            return actor;
        }

        private Entity<GameScope> Create(LoadoutConfig loadout)
        {
            return CreateEntity.Empty()
                    .Add<Actor>()
                    .Add<HandSize, int>(loadout.HandSize)
                    .Chain(a => CreateDeck(a, loadout))
                    .Chain(a => CreateCardsOnField(a, loadout))
                ;
        }

        private Entity<GameScope> CreateDeck(Entity<GameScope> actor, LoadoutConfig loadout)
        {
            var deck = CardFactory.CreateDeckWithCards(loadout.Deck);

            foreach (var card in DeckUtils.GetAllCardsInDeck(deck.ID()))
                card.Is<Fella>(card.Is<UnitCard>() && !card.Is<Leader>());

            return actor;
        }

        private Entity<GameScope> CreateCardsOnField(Entity<GameScope> actor, LoadoutConfig loadout)
        {
            foreach (var (id, coordinates) in loadout.UnitsOnField)
                CardFactory.CreateCardOnCoordinates(id, coordinates);

            return actor;
        }
    }
}
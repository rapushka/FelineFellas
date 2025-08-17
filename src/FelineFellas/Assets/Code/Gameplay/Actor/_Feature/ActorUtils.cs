using System.Collections.Generic;
using System.Linq;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public static class ActorUtils
    {
        private static readonly SideGroups<PlayerCard> Player = new();
        private static readonly SideGroups<EnemyCard> Enemy = new();

        public static bool HasFullHand(Entity<GameScope> actor)
            => GetCardsInHand(actor).count >= actor.Get<HandSize>().Value;

        public static IGroup<Entity<GameScope>> GetCardsInHand(Entity<GameScope> actor)
            => actor.Get<OnSide>().Value.Visit(
                onPlayer: () => Player.CardsInHand,
                onEnemy: () => Enemy.CardsInHand
            );

        public static bool HasAnyCardInDiscard(Entity<GameScope> actor)
            => GetDiscardedCards(actor).Any();

        public static IGroup<Entity<GameScope>> GetDiscardedCards(Entity<GameScope> actor)
            => actor.Get<OnSide>().Value.Visit(
                onPlayer: () => Player.CardsInDiscard,
                onEnemy: () => Enemy.CardsInDiscard
            );

        public static bool HasAnyCardInDeck(Entity<GameScope> actor) => GetCardsInDeck(actor).Any();

        public static HashSet<Entity<GameScope>> GetCardsInDeck(Entity<GameScope> actor)
        {
            var deckID = actor.GetOwnedDeck().ID();
            return DeckUtils.GetAllCardsInDeck(deckID);
        }

        private class SideGroups<TComponent>
            where TComponent : FlagComponent, IInScope<GameScope>, new()
        {
            public IGroup<Entity<GameScope>> CardsInHand
                => GroupBuilder<GameScope>
                    .With<Card>()
                    .And<InHandIndex>()
                    .And<TComponent>()
                    .Build();

            public IGroup<Entity<GameScope>> CardsInDiscard
                => GroupBuilder<GameScope>
                    .With<Card>()
                    .And<InDiscard>()
                    .And<TComponent>()
                    .Build();
        }
    }
}
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public static class ActorUtils
    {
        private static readonly SideGroups<PlayerCard> Player = new();
        private static readonly SideGroups<EnemyCard> Enemy = new();

        public static bool IsActorHandFull(Entity<GameScope> actor)
            => GetCardsInHandOfActor(actor).count >= actor.Get<HandSize>().Value;

        public static IGroup<Entity<GameScope>> GetCardsInHandOfActor(Entity<GameScope> actor)
            => actor.Get<OnSide>().Value.Visit(
                onPlayer: () => Player.CardsInHand,
                onEnemy: () => Enemy.CardsInHand
            );

        private class SideGroups<TComponent>
            where TComponent : FlagComponent, IInScope<GameScope>, new()
        {
            public IGroup<Entity<GameScope>> CardsInHand
                => GroupBuilder<GameScope>
                    .With<Card>()
                    .And<InHandIndex>()
                    .And<TComponent>()
                    .Build();
        }
    }
}
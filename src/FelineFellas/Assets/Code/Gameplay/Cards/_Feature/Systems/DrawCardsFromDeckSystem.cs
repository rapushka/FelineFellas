using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class DrawCardsFromDeckSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _actors
            = GroupBuilder<GameScope>
                .With<Actor>()
                .And<ActiveActor>()
                .And<DrawingCards>()
                .And<HandSize>()
                .Without<NeedsShuffle>()
                .Without<WaitingForDeckShuffle>()
                .Without<ShufflingDeckTimer>()
                .Build();

        private static IRandomService RandomService => ServiceLocator.Resolve<IRandomService>();

        public void Execute()
        {
            foreach (var actor in _actors)
            {
                var actorID = actor.ID();

                var cardsInDeck = ActorUtils.GetCardsInDeck(actor);
                if (cardsInDeck.Count == 0)
                    continue;

                var card = RandomService.PickRandom(cardsInDeck);

                CardUtils.DrawCardToHand(card, ActorUtils.GetCardsInHand(actor).count)
                    .Set<ChildOf, EntityID>(actorID);

                CreateEntity.Empty()
                    .Add<RecalculateInHandIndexes>()
                    ;
            }
        }
    }
}
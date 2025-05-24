using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class DrawCardsFromDeckSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _actors
            = GroupBuilder<GameScope>
                .With<Actor>()
                .And<DrawingCardsActor>()
                .And<HandSize>()
                .Without<WaitingForDeckShuffle>()
                .Build();

        private static IRandomService RandomService => ServiceLocator.Resolve<IRandomService>();

        public void Execute()
        {
            foreach (var actor in _actors)
            {
                var actorID = actor.ID();

                var card = RandomService.PickRandom(ActorUtils.GetCardsInDeck(actor));

                CardUtils.DrawCardToHand(card, ActorUtils.GetCardsInHand(actor).count)
                    .Set<ChildOf, EntityID>(actorID);

                CreateEntity.Empty()
                    .Add<RecalculateInHandIndexes>()
                    ;
            }
        }
    }
}
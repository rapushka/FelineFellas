using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class StopDrawingCardsIfNoCardsLeftSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _actors
            = GroupBuilder<GameScope>
                .With<Actor>()
                .And<DrawingCardsActor>()
                .Without<WaitingForDeckShuffle>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(4);

        public void Execute()
        {
            foreach (var actor in _actors.GetEntities(_buffer))
            {
                var anyCardInDeck = ActorUtils.HasAnyCardInDeck(actor);
                var anyCardInDiscard = ActorUtils.HasAnyCardInDiscard(actor);

                if (!anyCardInDeck && !anyCardInDiscard)
                    actor.Is<DrawingCardsActor>(false);
            }
        }
    }
}
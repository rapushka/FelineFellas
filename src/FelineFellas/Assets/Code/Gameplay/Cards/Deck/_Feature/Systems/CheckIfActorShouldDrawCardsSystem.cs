using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CheckIfActorShouldDrawCardsSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<InDrawCardsState>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _actors
            = GroupBuilder<GameScope>
                .With<Actor>()
                .And<HandSize>()
                .Build();

        public void Execute()
        {
            foreach (var _ in _events)
            foreach (var actor in _actors)
            {
                var isActorHandFull = ActorUtils.HasFullHand(actor);
                actor
                    .Is<HasFullHand>(isActorHandFull)
                    .Is<DrawingCardsActor>(!isActorHandFull)
                    ;
            }
        }
    }
}
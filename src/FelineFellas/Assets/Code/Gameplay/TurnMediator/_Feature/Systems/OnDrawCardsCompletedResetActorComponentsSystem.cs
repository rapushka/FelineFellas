using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class OnDrawCardsCompletedResetActorComponentsSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _turnMediators
            = GroupBuilder<GameScope>
                .With<TurnMediator>()
                .And<InDrawCardsState>()
                .And<ToNextTurnState>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _actors
            = GroupBuilder<GameScope>
                .With<Actor>()
                .Build();

        public void Execute()
        {
            foreach (var _ in _turnMediators)
            foreach (var actor in _actors)
            {
                actor
                    .Is<HasFullHand>(false)
                    .Is<DrawingCardsActor>(false)
                    ;

                var deck = actor.Get<OwnedDeck>().Value.GetEntity();
                deck
                    .Is<NeedsShuffle>(false)
                    .RemoveSafely<ShufflingDeckTimer>()
                    ;
            }
        }
    }
}
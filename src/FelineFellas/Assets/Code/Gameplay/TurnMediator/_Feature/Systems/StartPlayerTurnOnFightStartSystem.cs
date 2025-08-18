using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class StartPlayerTurnOnFightStartSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<StartFightEvent>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _mediators
            = GroupBuilder<GameScope>
                .With<TurnMediator>()
                .Build();

        public void Execute()
        {
            foreach (var e in _events)
            foreach (var mediator in _mediators)
            {
                mediator
                    .Add<OnPlayerTurnStartedState>()
                    .Add<InitTurnState>()
                    ;

                e.Add<Destroy>();
            }
        }
    }
}
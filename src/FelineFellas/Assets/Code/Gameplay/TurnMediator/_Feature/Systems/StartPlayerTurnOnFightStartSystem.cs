using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class StartPlayerTurnOnFightStartSystem : IInitializeSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<StartFight>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _mediators
            = GroupBuilder<GameScope>
                .With<TurnMediator>()
                .Build();

        public void Initialize()
        {
            foreach (var _ in _events)
            foreach (var mediator in _mediators)
            {
                mediator
                    .Add<OnPlayerTurnStartedState>()
                    ;
            }
        }
    }
}
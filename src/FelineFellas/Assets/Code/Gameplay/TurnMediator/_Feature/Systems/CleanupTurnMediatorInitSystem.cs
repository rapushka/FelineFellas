using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CleanupTurnMediatorInitSystem : ICleanupSystem
    {
        private readonly IGroup<Entity<GameScope>> _turnMediators
            = GroupBuilder<GameScope>
                .With<TurnMediator>()
                .And<InitTurnState>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(4);

        public void Cleanup()
        {
            foreach (var turnMediator in _turnMediators.GetEntities(_buffer))
                turnMediator.Remove<InitTurnState>();
        }
    }
}
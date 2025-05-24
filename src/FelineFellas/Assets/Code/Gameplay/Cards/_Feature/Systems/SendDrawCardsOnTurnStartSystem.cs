using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class SendDrawCardsOnTurnStartSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<OnPlayerTurnStartedState>()
                .Build();

        public void Execute()
        {
            foreach (var _ in _events)
            {
                CreateEntity.Empty()
                    .Add<DrawCardsEvent>()
                    ;
            }
        }
    }
}
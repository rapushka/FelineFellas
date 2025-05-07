using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public class OnPlayerTurnEndedStartEnemyTurnSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<EndTurnEvent>()
                .Build();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static TurnsConfig.ViewConfig TurnsViewConfig => GameConfig.Turns.View;

        public void Execute()
        {
            foreach (var _ in _events)
            {
                CreateEntity.Empty()
                    .Add<EnemyTurn, float>(TurnsViewConfig.EnemyTurnDuration)
                    ;
            }
        }
    }
}
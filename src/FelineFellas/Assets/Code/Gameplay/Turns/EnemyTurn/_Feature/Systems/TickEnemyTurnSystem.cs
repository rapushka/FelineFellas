using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public class TickEnemyTurnSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _turns
            = GroupBuilder<GameScope>
                .With<EnemyTurn>()
                .Build();

        private static ITimeService TimeService => ServiceLocator.Resolve<ITimeService>();

        public void Execute()
        {
            foreach (var turn in _turns)
            {
                var timeLeft = turn.Get<EnemyTurn, float>();
                timeLeft -= TimeService.AnimationDelta;
                turn.Set<EnemyTurn, float>(timeLeft);

                if (timeLeft <= 0f)
                {
                    turn.Add<Destroy>();

                    CreateEntity.OneFrame()
                        .Add<StartTurnEvent>()
                        ;
                }
            }
        }
    }
}
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

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static CardsConfig.ViewConfig ViewConfig => GameConfig.Cards.View;

        public void Execute()
        {
            foreach (var turn in _turns)
            {
                var timeLeft = turn.Get<EnemyTurn, float>();
                timeLeft -= TimeService.AnimationDelta;
                turn.Set<EnemyTurn, float>(timeLeft);

                if (!(timeLeft <= 0f))
                    continue;

                turn
                    .Add<Destroy>()
                    .Add<EnemyTurnEnded>()
                    ;

                CreateEntity.Empty()
                    .Add<DelayBeforeStartTurn, float>(ViewConfig.CardsDiscardDuration)
                    ;
            }
        }
    }
}
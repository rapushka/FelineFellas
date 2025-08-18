using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public class AnnounceCurrentTurnUiSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _turnMediators
            = GroupBuilder<GameScope>
                .With<TurnMediator>()
                .And<ToNextTurnState>()
                .Or<OnPlayerTurnStartedState>()
                .Or<OnEnemyTurnStartedState>()
                .Build();

        private static IUiMediator UiMediator => ServiceLocator.Resolve<IUiMediator>();

        private static GameplayHUD HUD => UiMediator.Pages.GetCurrent<GameplayHUD>();

        public void Execute()
        {
            foreach (var turnMediator in _turnMediators)
            {
                if (turnMediator.Is<OnPlayerTurnStartedState>())
                    HUD.TurnAnnounce.OnPlayerTurnStarted();
                else if (turnMediator.Is<OnEnemyTurnStartedState>())
                    HUD.TurnAnnounce.OnEnemyTurnStarted();
            }
        }
    }
}
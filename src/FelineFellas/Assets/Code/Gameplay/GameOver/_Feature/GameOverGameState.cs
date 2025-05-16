namespace FelineFellas
{
    public class GameOverGameState : IGameState
    {
        private static IUiMediator UiMediator => ServiceLocator.Resolve<IUiMediator>();

        public void OnEnter(GameStateMachine stateMachine)
        {
            UiMediator.Pages.OpenGameOver();
        }
    }
}
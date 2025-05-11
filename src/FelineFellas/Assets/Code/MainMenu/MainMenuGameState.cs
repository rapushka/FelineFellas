namespace FelineFellas
{
    public class MainMenuGameState : IGameState, IExitState
    {
        private static IUiMediator UiMediator => ServiceLocator.Resolve<IUiMediator>();

        public void OnEnter(GameStateMachine stateMachine)
        {
            UiMediator.Pages.OpenMainMenu();
        }

        public void OnExit()
        {
            UiMediator.Pages.HideAll();
        }
    }
}
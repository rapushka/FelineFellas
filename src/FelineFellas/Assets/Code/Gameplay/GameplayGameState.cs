namespace FelineFellas
{
    public class GameplayGameState : IGameState, IExitState, IUpdatableState
    {
        private static IEcsRunner EcsRunner => ServiceLocator.Resolve<IEcsRunner>();

        private static IInputService InputService => ServiceLocator.Resolve<IInputService>();

        private static ITimeService TimeService => ServiceLocator.Resolve<ITimeService>();

        private static IIdentifiesService IdService => ServiceLocator.Resolve<IIdentifiesService>();

        private static IUiMediator UiMediator => ServiceLocator.Resolve<IUiMediator>();

        public void OnEnter(GameStateMachine stateMachine)
        {
            UiMediator.Pages.OpenGameplay();

            EcsRunner.StartGame();
        }

        public void OnUpdate()
        {
            var deltaTime = TimeService.RealDelta;

            EcsRunner.OnUpdate();
            InputService.OnUpdate(deltaTime);
        }

        public void OnExit()
        {
            EcsRunner.EndGame();
            IdService.Reset();

            UiMediator.Pages.HideAll();
        }
    }
}
namespace FelineFellas
{
    public class GameplayGameState : IGameState, IExitState, IUpdatableState
    {
        private readonly ParenthoodDebugger _debugger = new();

        private static IEcsRunner EcsRunner => ServiceLocator.Resolve<IEcsRunner>();

        private static IInputService InputService => ServiceLocator.Resolve<IInputService>();

        private static ITimeService TimeService => ServiceLocator.Resolve<ITimeService>();

        private static IIdentifiesService IdService => ServiceLocator.Resolve<IIdentifiesService>();

        private static IUiMediator UiMediator => ServiceLocator.Resolve<IUiMediator>();

        private static IGameStateMachine GameStateMachine => ServiceLocator.Resolve<IGameStateMachine>();

        public void OnEnter(GameStateMachine stateMachine)
        {
            UiMediator.Pages.OpenGameplay();

            EcsRunner.StartGame();

            _debugger.Initialize();
        }

        public void OnUpdate()
        {
            var deltaTime = TimeService.RealDelta;

            InputService.OnUpdate(deltaTime);
            EcsRunner.OnUpdate();

            GameStateMachine.CheckPendingState();

            _debugger.OnUpdate();
        }

        public void OnExit()
        {
            EcsRunner.EndGame();
            IdService.Reset();

            UiMediator.Pages.HideAll();
        }
    }
}
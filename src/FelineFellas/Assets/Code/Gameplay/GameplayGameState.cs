namespace FelineFellas
{
    public class GameplayGameState : IGameState, IExitState, IUpdatableState
    {
        private static IEcsRunner EcsRunner => ServiceLocator.Resolve<IEcsRunner>();

        private static IInputService InputService => ServiceLocator.Resolve<IInputService>();

        private static ITimeService TimeService => ServiceLocator.Resolve<ITimeService>();

        public void OnEnter(GameStateMachine stateMachine)
        {
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
        }
    }
}
namespace FelineFellas
{
    public class GameplayGameState : IGameState, IExitState, IUpdatableState
    {
        private static IEcsRunner EcsRunner => ServiceLocator.Resolve<IEcsRunner>();

        public void OnEnter(GameStateMachine stateMachine)
        {
            EcsRunner.StartGame();
        }

        public void OnUpdate()
        {
            EcsRunner.OnUpdate();
        }

        public void OnExit()
        {
            EcsRunner.EndGame();
        }
    }
}
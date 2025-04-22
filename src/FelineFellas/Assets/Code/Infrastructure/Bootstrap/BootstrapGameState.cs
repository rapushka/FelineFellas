namespace FelineFellas
{
    public class BootstrapGameState : IGameState
    {
        private static IEcsRunner EcsRunner => ServiceLocator.Resolve<IEcsRunner>();

        public void OnEnter(GameStateMachine stateMachine)
        {
            EcsRunner.Initialize();

            stateMachine.ToState<GameplayGameState>();
        }
    }
}
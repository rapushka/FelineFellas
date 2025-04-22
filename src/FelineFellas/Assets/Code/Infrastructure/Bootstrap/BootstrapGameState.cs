namespace FelineFellas
{
    public class BootstrapGameState : IGameState
    {
        private static IEcsRunner EcsRunner => ServiceLocator.Resolve<IEcsRunner>();

        private static IViewFactory ViewFactory => ServiceLocator.Resolve<IViewFactory>();

        public void OnEnter(GameStateMachine stateMachine)
        {
            EcsRunner.Initialize();
            ViewFactory.Initialize();

            stateMachine.ToState<GameplayGameState>();
        }
    }
}
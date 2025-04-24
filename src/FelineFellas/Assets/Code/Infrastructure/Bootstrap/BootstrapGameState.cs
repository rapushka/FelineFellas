namespace FelineFellas
{
    public class BootstrapGameState : IGameState
    {
        private static IEcsRunner EcsRunner => ServiceLocator.Resolve<IEcsRunner>();

        private static IViewFactory ViewFactory => ServiceLocator.Resolve<IViewFactory>();

        private static ICamerasService CamerasService => ServiceLocator.Resolve<ICamerasService>();

        public void OnEnter(GameStateMachine stateMachine)
        {
            EcsRunner.Initialize();
            ViewFactory.Initialize();
            CamerasService.Initialize();

            stateMachine.ToState<GameplayGameState>();
        }
    }
}
namespace FelineFellas
{
    public class BootstrapGameState : IGameState
    {
        private static IEcsRunner EcsRunner => ServiceLocator.Resolve<IEcsRunner>();

        private static IViewFactory ViewFactory => ServiceLocator.Resolve<IViewFactory>();

        private static ICamerasService CamerasService => ServiceLocator.Resolve<ICamerasService>();

        private static IUIService UIService => ServiceLocator.Resolve<IUIService>();

        private static IPagesService Pages => ServiceLocator.Resolve<IPagesService>();

        public void OnEnter(GameStateMachine stateMachine)
        {
            EcsRunner.Initialize();
            ViewFactory.Initialize();
            CamerasService.Initialize();
            UIService.Initialize();
            (Pages as IInitializableService)?.Initialize();

            stateMachine.ToState<MainMenuGameState>();
        }
    }
}
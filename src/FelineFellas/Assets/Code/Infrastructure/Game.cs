namespace FelineFellas
{
    public class Game
    {
        private static Game _instance;

        public static Game Instance => _instance ??= new();

        private Game() { }

        private static IGameStateMachine StateMachine => ServiceLocator.Resolve<IGameStateMachine>();

        public void RegisterServices(IGameConfig gameConfig)
        {
            // ReSharper disable once RedundantTypeArgumentsOfMethod – keep for consistency
            ServiceLocator.Register<IGameConfig>(gameConfig);
            ServiceLocator.Register<IGameStateMachine>(new GameStateMachine());
            ServiceLocator.Register<IEcsRunner>(new EcsRunner());
            ServiceLocator.Register<IInputService>(new InputService());
            ServiceLocator.Register<ICamerasService>(new CamerasService(gameConfig.Cameras));

            // Factories
            ServiceLocator.Register<IViewFactory>(new ViewFactory());
            ServiceLocator.Register<IFieldFactory>(new FieldFactory());
            ServiceLocator.Register<ICardFactory>(new CardFactory());
        }

        public void Run()
        {
            StateMachine.ToState<BootstrapGameState>();
        }

        public void OnUpdate()
        {
            StateMachine.OnUpdate();
        }
    }
}
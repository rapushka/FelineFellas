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
            ServiceLocator.Register<ICamerasService>(new CamerasService(gameConfig.CameraDirectorPrefab));
            ServiceLocator.Register<ITimeService>(new TimeService());
            ServiceLocator.Register<IIdentifiesService>(new SimplestIdentifiesService());
            ServiceLocator.Register<IUIService>(new UIService());
            ServiceLocator.Register<IUiMediator>(new UiMediator());
            ServiceLocator.Register<IPagesService>(new PagesService());
            ServiceLocator.Register<IGameModeService>(new GameModeService());
            ServiceLocator.Register<IRandomService>(new RandomService());

            // Factories
            ServiceLocator.Register<IViewFactory>(new ViewFactory());
            ServiceLocator.Register<IFieldFactory>(new FieldFactory());
            ServiceLocator.Register<ICardFactory>(new CardFactory());
            ServiceLocator.Register<IShopFactory>(new ShopFactory());
            ServiceLocator.Register<IActorFactory>(new ActorFactory());
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
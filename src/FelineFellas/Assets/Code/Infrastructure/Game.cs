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
        }

        public void Run()
        {
            StateMachine.ToState<BootstrapGameState>();
        }
    }
}
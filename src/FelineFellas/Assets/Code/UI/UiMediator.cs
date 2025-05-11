namespace FelineFellas
{
    public interface IUiMediator : IService
    {
        IPagesService Pages { get; }

        void StartGame();

        void EndTurn();
    }

    public class UiMediator : IUiMediator
    {
        private static IPagesService PagesService => ServiceLocator.Resolve<IPagesService>();

        private static IGameStateMachine GameStateMachine => ServiceLocator.Resolve<IGameStateMachine>();

        public IPagesService Pages => PagesService;

        public void StartGame()
        {
            GameStateMachine.ToState<GameplayGameState>();
        }

        public void EndTurn()
        {
            CreateEntity.OneFrame()
                .Add<EndTurnEvent>()
                ;
        }
    }
}
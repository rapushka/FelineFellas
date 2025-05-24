using Entitas.Generic;

namespace FelineFellas
{
    public interface IUiMediator : IService
    {
        IPagesService Pages { get; }

        void StartGame(IGameMode gameMode);
        void GameOver();
        void ToMainMenu();

        void EndTurn();
    }

    public class UiMediator : IUiMediator
    {
        private static IPagesService PagesService => ServiceLocator.Resolve<IPagesService>();

        private static IGameModeService  GameModeService  => ServiceLocator.Resolve<IGameModeService>();
        private static IGameStateMachine GameStateMachine => ServiceLocator.Resolve<IGameStateMachine>();

        public IPagesService Pages => PagesService;

        private static Entity<GameScope> TurnMediator
            => Contexts.Instance.Get<GameScope>().Unique.GetEntity<TurnMediator>();

        public void StartGame(IGameMode gameMode)
        {
            GameModeService.SetGameMode(gameMode);
            GameStateMachine.ToState<GameplayGameState>();
        }

        public void EndGame()
        {
            GameStateMachine.ToState<MainMenuGameState>();
        }

        public void GameOver() { }

        public void ToMainMenu()
        {
            GameStateMachine.ToState<MainMenuGameState>();
        }

        public void EndTurn()
        {
            TurnMediator.Add<ToNextTurnState>();
        }
    }
}
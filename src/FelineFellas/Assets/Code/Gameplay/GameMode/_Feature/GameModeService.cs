namespace FelineFellas
{
    public interface IGameModeService : IService
    {
        IGameMode CurrentGameMode { get; }

        void SetGameMode(IGameMode gameMode);
    }

    public class GameModeService : IGameModeService
    {
        public IGameMode CurrentGameMode { get; private set; }

        public void SetGameMode(IGameMode gameMode)
        {
            CurrentGameMode = gameMode;
        }
    }
}
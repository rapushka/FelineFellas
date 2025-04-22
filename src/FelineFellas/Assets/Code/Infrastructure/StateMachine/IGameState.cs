namespace FelineFellas
{
    public interface IGameState
    {
        void OnEnter(GameStateMachine stateMachine);
    }

    public interface IExitState
    {
        void OnExit();
    }

    public interface IUpdatableState
    {
        void OnUpdate();
    }
}
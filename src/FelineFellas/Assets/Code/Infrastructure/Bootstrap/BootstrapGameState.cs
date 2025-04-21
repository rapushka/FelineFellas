namespace FelineFellas
{
    public class BootstrapGameState : IGameState
    {
        public void OnEnter(GameStateMachine stateMachine)
        {
            stateMachine.ToState<GameplayGameState>();
        }
    }
}
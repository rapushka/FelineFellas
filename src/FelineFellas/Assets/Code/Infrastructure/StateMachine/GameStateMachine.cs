namespace FelineFellas
{
    public interface IGameStateMachine : IService
    {
        void ToState<TState>() where TState : IGameState, new();
    }

    public class GameStateMachine : IGameStateMachine
    {
        private readonly TypeDictionary<IGameState> _statesMap = new();

        private IGameState _currentState;

        public void ToState<TState>()
            where TState : IGameState, new()
        {
            // ReSharper disable once SuspiciousTypeConversion.Global – there will be some, believe me
            (_currentState as IExitState)?.OnExit();

            _currentState = Get<TState>();
            _currentState.OnEnter(this);
        }

        private TState Get<TState>()
            where TState : IGameState, new()
            => _statesMap.GetOrAdd(() => new TState());
    }
}
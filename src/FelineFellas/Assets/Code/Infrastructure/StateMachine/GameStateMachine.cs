namespace FelineFellas
{
    public interface IGameStateMachine : IService
    {
        void ToState<TState>() where TState : IGameState, new();

        void PendState<TState>() where TState : IGameState, new();
        void CheckPendingState();

        void OnUpdate();
    }

    public class GameStateMachine : IGameStateMachine
    {
        private readonly TypeDictionary<IGameState> _statesMap = new();

        private IGameState _currentState;
        private IGameState _pendingState;

        public void ToState<TState>()
            where TState : IGameState, new()
        {
            ToState(Get<TState>());
        }

        public void PendState<TState>() where TState : IGameState, new()
            => _pendingState = Get<TState>();

        public void CheckPendingState()
        {
            if (_pendingState is null)
                return;

            ToState(_pendingState);
            _pendingState = null;
        }

        public void OnUpdate()
        {
            (_currentState as IUpdatableState)?.OnUpdate();
        }

        private void ToState(IGameState nextState)
        {
            (_currentState as IExitState)?.OnExit();

            _currentState = nextState;
            _currentState.OnEnter(this);
        }

        private TState Get<TState>()
            where TState : IGameState, new()
            => _statesMap.GetOrAdd(() => new TState());
    }
}
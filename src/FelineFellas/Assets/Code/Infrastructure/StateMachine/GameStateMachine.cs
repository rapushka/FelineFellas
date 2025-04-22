namespace FelineFellas
{
    public interface IGameStateMachine : IService
    {
        void ToState<TState>() where TState : IGameState, new();

        void OnUpdate();
    }

    public class GameStateMachine : IGameStateMachine
    {
        private readonly TypeDictionary<IGameState> _statesMap = new();

        private IGameState _currentState;

        public void ToState<TState>()
            where TState : IGameState, new()
        {
            (_currentState as IExitState)?.OnExit();

            _currentState = Get<TState>();
            _currentState.OnEnter(this);
        }

        public void OnUpdate()
        {
            (_currentState as IUpdatableState)?.OnUpdate();
        }

        private TState Get<TState>()
            where TState : IGameState, new()
            => _statesMap.GetOrAdd(() => new TState());
    }
}
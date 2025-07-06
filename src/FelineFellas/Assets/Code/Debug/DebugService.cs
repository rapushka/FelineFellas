namespace FelineFellas
{
    public interface IDebugService : IService
    {
        void Initialize();

        void OnUpdate();
    }

    public class DebugService : IDebugService
    {
        private readonly ParenthoodDebugger _debugger = new();

        public void Initialize()
        {
            _debugger.Initialize();
        }

        public void OnUpdate()
        {
            _debugger.OnUpdate();
        }
    }
}
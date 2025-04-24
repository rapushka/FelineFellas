using Entitas.Generic;

namespace FelineFellas
{
    public interface IEcsRunner : IService
    {
        void Initialize();

        void StartGame();

        void OnUpdate();

        void EndGame();
    }

    public class EcsRunner : IEcsRunner
    {
        private GameplayFeature _feature;

        public void Initialize()
        {
            Contexts.Instance.InitializeScope<GameScope>();
            Contexts.Instance.InitializeScope<InputScope>();

            _feature = new();
        }

        public void StartGame()
        {
            _feature.Initialize();
        }

        public void OnUpdate()
        {
            _feature.Execute();
            _feature.Cleanup();
        }

        public void EndGame()
        {
            _feature.DeactivateReactiveSystems();
            _feature.ClearReactiveSystems();

            DisposeEntities();

            _feature.Cleanup();
            _feature.TearDown();
        }

        private void DisposeEntities() { }
    }
}
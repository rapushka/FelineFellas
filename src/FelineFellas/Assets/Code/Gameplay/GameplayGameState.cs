using UnityEngine;

namespace FelineFellas
{
    public class GameplayGameState : IGameState, IExitState, IUpdatableState
    {
        private static IEcsRunner EcsRunner => ServiceLocator.Resolve<IEcsRunner>();

        private static IInputService InputService => ServiceLocator.Resolve<IInputService>();

        private static ITimeService TimeService => ServiceLocator.Resolve<ITimeService>();

        private static IIdentifiesService IdService => ServiceLocator.Resolve<IIdentifiesService>();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static IUIService UIService => ServiceLocator.Resolve<IUIService>();

        private GameplayHUD _hud;

        public void OnEnter(GameStateMachine stateMachine)
        {
            _hud = Object.Instantiate(GameConfig.UI.HUDPrefab, UIService.CanvasRoot);

            EcsRunner.StartGame();
        }

        public void OnUpdate()
        {
            var deltaTime = TimeService.RealDelta;

            EcsRunner.OnUpdate();
            InputService.OnUpdate(deltaTime);
        }

        public void OnExit()
        {
            EcsRunner.EndGame();
            IdService.Reset();

            _hud.DestroyObject();
        }
    }
}
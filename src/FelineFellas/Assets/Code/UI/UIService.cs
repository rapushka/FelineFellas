using UnityEngine;
using Object = UnityEngine.Object;

namespace FelineFellas
{
    public interface IUIService : IService
    {
        RectTransform CanvasRoot { get; }

        void Initialize();
    }

    public class UIService : IUIService
    {
        private Canvas _canvas;

        public RectTransform CanvasRoot => (RectTransform)_canvas.transform;

        private static ICamerasService CamerasService => ServiceLocator.Resolve<ICamerasService>();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        public void Initialize()
        {
            _canvas = Object.Instantiate(GameConfig.UI.CanvasPrefab);
            _canvas.worldCamera = CamerasService.UICamera;
        }
    }
}
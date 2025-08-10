using UnityEngine;

namespace FelineFellas
{
    public interface ICamerasService : IService
    {
        Camera UICamera { get; }

        void Initialize();

        Vector2 ScreenToWorld(Vector2 screenPosition);

        Vector2 WorldToScreen(Vector2 worldPosition);
    }

    public class CamerasService : ICamerasService
    {
        private readonly CameraDirector _cameraDirectorPrefab;
        private CameraDirector _cameraDirector;

        public CamerasService(CameraDirector cameraDirectorPrefab)
        {
            _cameraDirectorPrefab = cameraDirectorPrefab;
        }

        public Camera UICamera => _cameraDirector.UICamera;

        private Camera MainCamera => _cameraDirector.MainCamera;

        public void Initialize()
        {
            _cameraDirector = Object.Instantiate(_cameraDirectorPrefab);
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition) => MainCamera.ScreenToWorldPoint(screenPosition);

        public Vector2 WorldToScreen(Vector2 worldPosition) => MainCamera.WorldToScreenPoint(worldPosition);
    }
}
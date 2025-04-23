using UnityEngine;

namespace FelineFellas
{
    public interface ICamerasService : IService
    {
        void Initialize();

        Vector2 ScreenToWorld(Vector2 screenPosition);
    }

    public class CamerasService : ICamerasService
    {
        private readonly CameraDirector _cameraDirectorPrefab;
        private CameraDirector _cameraDirector;

        public CamerasService(CameraDirector cameraDirectorPrefab)
        {
            _cameraDirectorPrefab = cameraDirectorPrefab;
        }

        private Camera MainCamera => _cameraDirector.MainCamera;

        public void Initialize()
        {
            _cameraDirector = Object.Instantiate(_cameraDirectorPrefab);
        }

        public Vector2 ScreenToWorld(Vector2 screenPosition) => MainCamera.ScreenToWorldPoint(screenPosition);
    }
}
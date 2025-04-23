using UnityEngine;

namespace FelineFellas
{
    public interface ICamerasService : IService
    {
        Vector2 ScreenToWorld(Vector2 screenPosition);
    }

    public class CamerasService : ICamerasService
    {
        private readonly CameraDirector _cameraDirector;

        public CamerasService(CameraDirector cameraDirector)
        {
            _cameraDirector = cameraDirector;
        }

        private Camera MainCamera => _cameraDirector.MainCamera;

        public Vector2 ScreenToWorld(Vector2 screenPosition) => MainCamera.ScreenToWorldPoint(screenPosition);
    }
}
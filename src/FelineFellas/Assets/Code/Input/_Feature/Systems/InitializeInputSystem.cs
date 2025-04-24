using Entitas;
using UnityEngine;

namespace FelineFellas
{
    public sealed class InitializeInputSystem : IInitializeSystem
    {
        private static IInputService   InputService   => ServiceLocator.Resolve<IInputService>();
        private static ICamerasService CamerasService => ServiceLocator.Resolve<ICamerasService>();

        public void Initialize()
        {
            var mouseWorldPosition = CamerasService.ScreenToWorld(InputService.MouseScreenPosition);

            CreateInputEntity.Empty()
                .Is<PlayerInput>(true)
                .Add<WorldPosition, Vector2>(mouseWorldPosition)
                ;
        }
    }
}
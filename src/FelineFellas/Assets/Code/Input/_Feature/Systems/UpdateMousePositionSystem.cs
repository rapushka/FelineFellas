using Entitas;
using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public sealed class UpdateMousePositionSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<InputScope>> _cursors
            = GroupBuilder<InputScope>
                .With<PlayerInput>()
                .And<WorldPosition>()
                .Build();

        private static IInputService   InputService   => ServiceLocator.Resolve<IInputService>();
        private static ICamerasService CamerasService => ServiceLocator.Resolve<ICamerasService>();

        public void Execute()
        {
            foreach (var cursor in _cursors)
            {
                var mouseWorldPosition = CamerasService.ScreenToWorld(InputService.MouseScreenPosition);

                cursor.Set<WorldPosition, Vector2>(mouseWorldPosition);
            }
        }
    }
}
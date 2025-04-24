using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class StartDraggingSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _hoveredEntities
            = GroupBuilder<GameScope>
                .With<Hovered>()
                .And<Draggable>()
                .Build();

        private readonly IGroup<Entity<InputScope>> _inputs
            = GroupBuilder<InputScope>
                .With<PlayerInput>()
                .And<CursorJustDown>()
                .Build();

        public void Execute()
        {
            foreach (var _ in _inputs)
            foreach (var entity in _hoveredEntities)
            {
                entity.Is<Dragging>(true);
            }
        }
    }
}
using Entitas;
using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public sealed class DragEntitySystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _draggedEntities
            = GroupBuilder<GameScope>
                .With<Dragging>()
                .Build();

        private readonly IGroup<Entity<InputScope>> _inputs
            = GroupBuilder<InputScope>
                .With<PlayerInput>()
                .And<CursorHolding>()
                .And<WorldPosition>()
                .Build();

        public void Execute()
        {
            foreach (var input in _inputs)
            foreach (var entity in _draggedEntities)
            {
                var cursorPosition = input.Get<WorldPosition, Vector2>();
                entity.Set<WorldPosition, Vector2>(cursorPosition);
            }
        }
    }
}
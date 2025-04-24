using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class DropEntitiesSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _hoveredEntities
            = GroupBuilder<GameScope>
                .With<Hovered>()
                .And<Dragging>()
                .Build();

        private readonly IGroup<Entity<InputScope>> _inputs
            = GroupBuilder<InputScope>
                .With<PlayerInput>()
                .Or<CursorJustUp>()
                .Or<CursorJustClicked>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(8);

        public void Execute()
        {
            foreach (var _ in _inputs)
            foreach (var entity in _hoveredEntities.GetEntities(_buffer))
            {
                entity
                    .Is<Dragging>(false)
                    .Add<Dropped>()
                    ;
            }
        }
    }
}
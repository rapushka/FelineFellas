using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UseUnitCardSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _cells
            = GroupBuilder<GameScope>
                .With<Cell>()
                .And<Collider>()
                .And<Empty>()
                .Build();

        private readonly IGroup<Entity<InputScope>> _inputs
            = GroupBuilder<InputScope>
                .With<PlayerInput>()
                .And<WorldPosition>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _draggedCard
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<UnitCard>()
                .And<Dragging>()
                .Build();

        public void Execute()
        {
            foreach (var cell in _cells)
            foreach (var input in _inputs)
            foreach (var card in _draggedCard)
            {
                var cursorPosition = input.Get<WorldPosition>().Value;
                var cellCollider = cell.Get<Collider>().Value;

                var cursorOnCell = cellCollider.OverlapPoint(cursorPosition);

                if (cursorOnCell)
                    card.Is<WillBeUsed>(true);
            }
        }
    }
}
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
            foreach (var field in _cells)
            foreach (var input in _inputs)
            foreach (var card in _draggedCard)
            {
                var cursorPosition = input.Get<WorldPosition>().Value;
                var fieldCollider = field.Get<Collider>().Value;

                var cursorOnField = fieldCollider.OverlapPoint(cursorPosition);
                card.Is<WillBeUsed>(cursorOnField);
            }
        }
    }
}
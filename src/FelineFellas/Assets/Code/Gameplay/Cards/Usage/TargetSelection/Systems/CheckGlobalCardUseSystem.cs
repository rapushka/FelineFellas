using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CheckGlobalCardUseSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _fields
            = GroupBuilder<GameScope>
                .With<Field>()
                .And<Collider>()
                .Build();

        private readonly IGroup<Entity<InputScope>> _inputs
            = GroupBuilder<InputScope>
                .With<PlayerInput>()
                .And<WorldPosition>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _draggedCard
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<GlobalCard>()
                .And<Dragging>()
                .Build();

        public void Execute()
        {
            foreach (var field in _fields)
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
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CheckActionCardUseSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _units
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<UnitCard>()
                .And<OnField>()
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
                .And<ActionCard>()
                .And<Dragging>()
                .Build();

        public void Execute()
        {
            foreach (var unit in _units)
            foreach (var input in _inputs)
            foreach (var card in _draggedCard)
            {
                var cursorPosition = input.Get<WorldPosition>().Value;
                var unitCollider = unit.Get<Collider>().Value;

                var cursorOnCell = unitCollider.OverlapPoint(cursorPosition);

                if (cursorOnCell)
                {
                    card
                        .Is<WillBeUsed>(true)
                        .Add<UseTarget, EntityID>(unit.ID())
                        ;
                }
            }
        }
    }
}
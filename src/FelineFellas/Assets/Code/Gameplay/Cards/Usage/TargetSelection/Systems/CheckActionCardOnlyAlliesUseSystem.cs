using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CheckActionCardOnlyAlliesUseSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _draggedCard
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<ActionCard>()
                .And<Dragging>()
                .And<OnlyForAllies>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _units
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<UnitCard>()
                .And<OnField>()
                .And<Collider>()
                .And<Ally>()
                .Build();

        private readonly IGroup<Entity<InputScope>> _inputs
            = GroupBuilder<InputScope>
                .With<PlayerInput>()
                .And<WorldPosition>()
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
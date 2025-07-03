using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CheckUnitCardUseSystem : IExecuteSystem
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

        private readonly IGroup<Entity<GameScope>> _draggedCards
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<UnitCard>()
                .And<Dragging>()
                .Build();

        public void Execute()
        {
            foreach (var card in _draggedCards)
            foreach (var cell in _cells)
            foreach (var cursor in _inputs)
            {
                if (card.Is<CanUseOnlyOnOurRow>() && !card.OnSameSide(cell))
                    continue;

                var cellCollider = cell.Get<Collider>().Value;
                var cursorOnUnit = cellCollider.OverlapPoint(cursor.WorldPosition());

                if (cursorOnUnit)
                {
                    card
                        .Is<WillBeUsed>(true)
                        .Add<DropCardOn, EntityID>(cell.ID())
                        ;
                }
            }
        }
    }
}
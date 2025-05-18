using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CheckActionCardUseSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _draggedCard
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<ActionCard>()
                .And<Dragging>()
                .Build();

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

        public void Execute()
        {
            foreach (var unit in _units)
            foreach (var input in _inputs)
            foreach (var card in _draggedCard)
            {
                var cursorPosition = input.Get<WorldPosition>().Value;
                var unitCollider = unit.Get<Collider>().Value;

                var cursorOnCell = unitCollider.OverlapPoint(cursorPosition);

                if (!cursorOnCell)
                    continue;

                var canUseOnEnemy = card.Is<CanUseOnEnemy>();
                var canUseOnFella = card.Is<CanUseOnFella>();
                var canUseOnLeader = card.Is<CanUseOnLeader>();

                if (unit.Is<Leader>() && !canUseOnLeader)
                    continue;

                if (unit.Is<Fella>() && !canUseOnFella)
                    continue;

                if (unit.Is<EnemyUnit>() && !canUseOnEnemy)
                    continue;

                card
                    .Is<WillBeUsed>(true)
                    .Add<UseTarget, EntityID>(unit.ID())
                    ;
            }
        }
    }
}
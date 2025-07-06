using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CheckOrderUseOnUnitSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _draggedCard
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<OrderCard>()
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
                var canUse = card.OnSameSide(unit)
                    && !unit.Is<OutOfStamina>()
                    && IsUnitTypeAllowed(card, unit)
                    && IsCursorOnUnit(input, unit);
                if (!canUse)
                    continue;

                card
                    .Is<WillBeUsed>(true)
                    .Add<DropCardOn, EntityID>(unit.ID())
                    ;
            }
        }

        private static bool IsUnitTypeAllowed(Entity<GameScope> card, Entity<GameScope> unit)
        {
            var canUseOnEnemy = card.Is<CanTargetSubjectEnemy>();
            var canUseOnFella = card.Is<CanTargetSubjectFella>();
            var canUseOnLeader = card.Is<CanTargetSubjectLeader>();

            return unit.Is<Leader>() && canUseOnLeader
                || unit.Is<Fella>() && canUseOnFella
                || unit.Is<EnemyUnit>() && canUseOnEnemy;
        }

        private static bool IsCursorOnUnit(Entity<InputScope> input, Entity<GameScope> unit)
        {
            var cursorPosition = input.Get<WorldPosition>().Value;
            var unitCollider = unit.Get<Collider>().Value;
            return unitCollider.OverlapPoint(cursorPosition);
        }
    }
}
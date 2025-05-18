using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class RequestClosestOpponentSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _cards
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<Used>()
                .And<TargetSelectClosestOpponent>()
                .And<UseTarget>()
                .Without<SelectedTarget>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _units
            = GroupBuilder<GameScope>
                .With<UnitCard>()
                .And<OnField>()
                .And<OnSide>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(32);

        public void Execute()
        {
            foreach (var card in _cards.GetEntities(_buffer))
            {
                var targetUnit = card.Get<UseTarget>().Value.GetEntity();
                var fromPosition = targetUnit.Get<OnField>().Value;

                float? closestDistance = null;
                Entity<GameScope> closestUnit = null;

                foreach (var otherUnit in _units)
                {
                    var onSameSide = targetUnit.OnSameSide(otherUnit);
                    if (onSameSide)
                        continue;

                    var otherUnitPosition = otherUnit.Get<OnField>().Value;
                    var distance = fromPosition.DistanceTo(otherUnitPosition);

                    if (closestDistance is null || closestDistance > distance)
                    {
                        closestDistance = distance;
                        closestUnit = otherUnit;
                    }
                }

                if (closestUnit is null)
                    continue;

                card.Add<SelectedTarget, EntityID>(closestUnit.ID());
            }
        }
    }
}
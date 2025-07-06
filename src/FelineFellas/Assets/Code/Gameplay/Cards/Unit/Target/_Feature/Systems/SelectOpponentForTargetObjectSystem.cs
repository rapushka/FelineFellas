using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    /// this is a placeholder, the Opponent system is not yet implemented
    // ReSharper disable once InconsistentNaming - i know!
    public sealed class SelectOpponentForTargetObjectSystem_TMP : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _abilities
            = GroupBuilder<GameScope>
                .With<AbilityUse>()
                .And<TargetObjectAsOpponent>()
                .And<TargetSubject>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _units
            = GroupBuilder<GameScope>
                .With<UnitCard>()
                .Build();

        public void Execute()
        {
            foreach (var ability in _abilities)
            {
                var sender = ability.Get<TargetSubject>().Value.GetEntity();
                var firstOpponent = _units.First(u => !u.OnSameSide(sender));

                ability.Set<TargetObject, EntityID>(firstOpponent.ID());
            }
        }
    }

    public sealed class SelectOpponentForTargetObjectSystem : IExecuteSystem
    {
        // private readonly IGroup<Entity<GameScope>> _cards
        //     = GroupBuilder<GameScope>
        //         .With<Card>()
        //         .And<Used>()
        //         .And<TargetSelectClosestOpponent>()
        //         .And<UseTarget>()
        //         .Without<SelectedTarget>()
        //         .Build();
        //
        // private readonly IGroup<Entity<GameScope>> _units
        //     = GroupBuilder<GameScope>
        //         .With<UnitCard>()
        //         .And<OnField>()
        //         .And<OnSide>()
        //         .Build();
        //
        // private readonly List<Entity<GameScope>> _buffer = new(32);

        public void Execute()
        {
            // TODO: FIXME
            // foreach (var card in _cards.GetEntities(_buffer))
            // {
            //     // var targetUnit = card.Get<UseTarget>().Value.GetEntity();
            //     // var fromPosition = targetUnit.Get<OnField>().Value;
            //     //
            //     // float? closestDistance = null;
            //     // Entity<GameScope> closestUnit = null;
            //     //
            //     // foreach (var otherUnit in _units)
            //     // {
            //     //     var onSameSide = targetUnit.OnSameSide(otherUnit);
            //     //     if (onSameSide)
            //     //         continue;
            //     //
            //     //     var otherUnitPosition = otherUnit.Get<OnField>().Value;
            //     //     var distance = fromPosition.DistanceTo(otherUnitPosition);
            //     //
            //     //     if (closestDistance is null || closestDistance > distance)
            //     //     {
            //     //         closestDistance = distance;
            //     //         closestUnit = otherUnit;
            //     //     }
            //     // }
            //     //
            //     // if (closestUnit is null)
            //     //     continue;
            //     //
            //     // card.Add<SelectedTarget, EntityID>(closestUnit.ID());
            // }
        }
    }
}
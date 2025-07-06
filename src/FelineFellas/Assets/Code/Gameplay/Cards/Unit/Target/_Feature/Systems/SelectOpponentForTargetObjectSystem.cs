using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class SelectOpponentForTargetObjectSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _abilities
            = GroupBuilder<GameScope>
                .With<AbilityUse>()
                .And<TargetObjectAsOpponent>()
                .And<TargetSubject>()
                .Build();

        public void Execute()
        {
            foreach (var ability in _abilities)
            {
                var sender = ability.Get<TargetSubject>().Value.GetEntity();
                var containingCell = sender.GetContainingCellID().GetEntity();
                var opposingCell = FieldUtils.GetOpposingCell(containingCell);

                // If Opposing Cell Has Unit - We don't need to seek for Leader
                if (opposingCell.TryGet<PlacedCard, EntityID>(out var opponentID))
                {
                    ability.Set<TargetObject, EntityID>(opponentID);
                    continue;
                }

                var targetID = SeekToOpponentLeader(opposingCell);
                ability.Set<TargetObject, EntityID>(targetID);
            }
        }

        private static EntityID SeekToOpponentLeader(Entity<GameScope> opposingCell)
        {
            var (side, index) = opposingCell.GetCellCoordinates();
            var targetCell = opposingCell;

            var opponentLeader = UnitUtils.GetLeader(side);
            var opponentLeaderCell = opponentLeader.GetContainingCellID().GetEntity();
            var directionToLeader = FieldUtils.GetDirection(opposingCell, opponentLeaderCell);

            EntityID opponentID;
            while (!targetCell.TryGet<PlacedCard, EntityID>(out opponentID))
            {
                index += directionToLeader;
                targetCell = FieldUtils.GetCellOrDefault(side, index);
            }

            return opponentID;
        }
    }
}
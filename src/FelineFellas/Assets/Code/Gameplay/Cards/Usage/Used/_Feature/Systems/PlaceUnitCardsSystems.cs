using System.Collections.Generic;
using Entitas;
using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public sealed class PlaceUnitCardsSystems : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _droppedCards
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<WillBeUsed>()
                .And<UnitCard>()
                .And<TargetCell>()
                .And<Dropped>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(8);

        public void Execute()
        {
            foreach (var card in _droppedCards.GetEntities(_buffer))
            {
                var cell = card.Get<TargetCell>().Value.GetEntity();

                CardUtils.MarkUsed(card)
                    .Set<TargetRotation, float>(0f)
                    .Set<TargetPosition, Vector2>(cell.Get<WorldPosition, Vector2>())
                    ;

                cell
                    .Is<Empty>(false)
                    .Set<PlacedUnit, EntityID>(card.ID())
                    ;
            }
        }
    }
}
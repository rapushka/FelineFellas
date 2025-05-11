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
                .And<UseTarget>()
                .And<Dropped>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(8);

        public void Execute()
        {
            foreach (var card in _droppedCards.GetEntities(_buffer))
            {
                var cell = card.Get<UseTarget>().Value.GetEntity();

                CardUtils.MarkUsed(card)
                    .Set<TargetRotation, float>(0f)
                    .Set<TargetPosition, Vector2>(cell.Get<WorldPosition, Vector2>())
                    .Set<OnField, Coordinates>(cell.Get<CellCoordinates>().Value)
                    ;

                cell
                    .Is<Empty>(false)
                    .Set<PlacedUnit, EntityID>(card.ID())
                    ;
            }
        }
    }
}
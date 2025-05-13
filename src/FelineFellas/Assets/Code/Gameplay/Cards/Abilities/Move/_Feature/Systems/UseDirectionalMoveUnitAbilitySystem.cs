using Entitas;
using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public sealed class UseDirectionalMoveUnitAbilitySystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _cards
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<Used>()
                .And<AbilityMove>()
                .And<UseTarget>()
                .And<TargetSelectNeighbor>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _fields
            = GroupBuilder<GameScope>
                .With<Field>()
                .And<FieldBorders>()
                .Build();

        private static PrimaryEntityIndex<GameScope, OnField, Coordinates> Index
            => Contexts.Instance.Get<GameScope>().GetPrimaryIndex<OnField, Coordinates>();

        public void Execute()
        {
            foreach (var card in _cards)
            foreach (var field in _fields)
            {
                var fieldBorders = field.Get<FieldBorders>().Value;

                var unit = card.Get<UseTarget>().Value.GetEntity();

                var unitCoordinates = unit.Get<OnField>().Value;
                var movement = card.Get<TargetSelectNeighbor>().Value;

                var targetCoordinates = unitCoordinates.Add(movement);
                targetCoordinates = fieldBorders.Clamp(targetCoordinates);

                var occupied = Index.HasEntity(targetCoordinates);

                if (occupied)
                {
                    Debug.Log("TODO: The Cell Is Already Occupied!");
                    continue;
                }

                CardUtils.PlaceCardOnGrid(unit, targetCoordinates);
            }
        }
    }
}
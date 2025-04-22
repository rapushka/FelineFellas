using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public class PositionView : BaseListener<GameScope, WorldPosition>
    {
        [SerializeField] private Transform _transform;

        public override void OnValueChanged(Entity<GameScope> entity, WorldPosition component)
        {
            _transform.position = component.Value;
        }
    }
}
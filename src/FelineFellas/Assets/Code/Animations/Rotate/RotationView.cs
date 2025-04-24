using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public class RotationView : BaseListener<GameScope, Rotation>
    {
        [SerializeField] private Transform _transform;

        public override void OnValueChanged(Entity<GameScope> entity, Rotation component)
        {
            _transform.SetEulerAngles(z: component.Value);
        }
    }
}
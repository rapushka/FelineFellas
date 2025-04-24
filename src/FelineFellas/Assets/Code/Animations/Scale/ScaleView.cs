using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public class ScaleView : BaseListener<GameScope, Scale>
    {
        [SerializeField] private Transform _transform;

        public override void OnValueChanged(Entity<GameScope> entity, Scale component)
        {
            _transform.localScale = new(component.Value, component.Value, 1);
        }
    }
}
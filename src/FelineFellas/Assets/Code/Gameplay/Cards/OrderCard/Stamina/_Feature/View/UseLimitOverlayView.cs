using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public class UseLimitOverlayView : BaseListener<GameScope, OutOfStamina>
    {
        [SerializeField] private GameObject _overlay;

        public override void OnValueChanged(Entity<GameScope> entity, OutOfStamina component)
        {
            _overlay.SetActive(entity.Is<OutOfStamina>());
        }
    }
}
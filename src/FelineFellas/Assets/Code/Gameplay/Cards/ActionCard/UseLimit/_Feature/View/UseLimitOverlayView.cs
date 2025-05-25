using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public class UseLimitOverlayView : BaseListener<GameScope, UseLimitReached>
    {
        [SerializeField] private GameObject _overlay;

        public override void OnValueChanged(Entity<GameScope> entity, UseLimitReached component)
        {
            _overlay.SetActive(entity.Is<UseLimitReached>());
        }
    }
}
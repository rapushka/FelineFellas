using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public class CardIconView : BaseListener<GameScope, CardIcon>
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public override void OnValueChanged(Entity<GameScope> entity, CardIcon component)
        {
            _spriteRenderer.sprite = component.Value;
        }
    }
}
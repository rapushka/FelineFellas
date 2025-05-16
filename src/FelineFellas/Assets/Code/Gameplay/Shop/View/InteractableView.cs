using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public class InteractableView : BaseListener<GameScope, Interactable>
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _defaultColor = Color.white;
        [SerializeField] private Color _disabledColor = Color.gray;

        public override void OnValueChanged(Entity<GameScope> entity, Interactable component)
        {
            _spriteRenderer.color = entity.Is<Interactable>()
                ? _defaultColor
                : _disabledColor;
        }
    }
}
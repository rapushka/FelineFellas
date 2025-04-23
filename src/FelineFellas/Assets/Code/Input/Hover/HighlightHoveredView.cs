using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public class HighlightHoveredView : BaseListener<GameScope, Hovered>
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _defaultColor = Color.white;
        [SerializeField] private Color _hoveredColor = Color.gray;

        public override void OnValueChanged(Entity<GameScope> entity, Hovered component)
        {
            _spriteRenderer.color = entity.Is<Hovered>()
                ? _hoveredColor
                : _defaultColor;
        }
    }
}
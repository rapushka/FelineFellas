using UnityEngine;

namespace FelineFellas
{
    [RequireComponent(typeof(Collider2D))]
    public class HoverReceiver : MonoBehaviour
    {
        [SerializeField] private GameEntityBehaviour _entityBehaviour;

        private bool IsHovered { set => _entityBehaviour.Entity?.Is<Hovered>(value); }

        private void OnMouseEnter()
        {
            IsHovered = true;
        }

        private void OnMouseExit()
        {
            IsHovered = false;
        }
    }
}
using UnityEngine;

namespace FelineFellas
{
    public interface IInputService : IService
    {
        Vector2 MouseScreenPosition { get; }
    }

    public class InputService : IInputService
    {
        public Vector2 MouseScreenPosition => Input.mousePosition;
    }
}
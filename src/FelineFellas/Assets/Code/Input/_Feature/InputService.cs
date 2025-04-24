using UnityEngine;

namespace FelineFellas
{
    public interface IInputService : IService
    {
        ButtonState LeftMouseButton { get; }

        Vector2 MouseScreenPosition { get; }

        void OnUpdate(float deltaTime);
    }

    public class InputService : IInputService
    {
        private readonly ButtonHandler _leftButtonHandler;

        public InputService()
        {
            _leftButtonHandler = new(this, MouseButton.Left);
        }

        public ButtonState LeftMouseButton => _leftButtonHandler.State;

        public Vector2 MouseScreenPosition => Input.mousePosition;

        public void OnUpdate(float deltaTime)
        {
            _leftButtonHandler.Update(deltaTime);
        }

        private class ButtonHandler
        {
            private readonly InputService _input;
            private readonly int _button;

            private float _holdTime;
            private Vector2 _startHoldPosition;
            private bool _isHolding;

            public ButtonHandler(InputService input, MouseButton button)
            {
                _input = input;
                _button = (int)button;
            }

            public ButtonState State { get; private set; } = ButtonState.Unknown;

            private Vector2 MousePosition => _input.MouseScreenPosition;

            public void Update(float deltaTime)
                => State = GetCurrentState(deltaTime);

            private ButtonState GetCurrentState(float deltaTime)
            {
                var justPressed = Input.GetMouseButtonDown(_button);
                var justReleased = Input.GetMouseButtonUp(_button);

                if (justPressed && justReleased) // ChatGPT says it's not possible, but it kinda is
                    return ButtonState.Clicked;

                if (justPressed)
                {
                    _holdTime = 0f;
                    _isHolding = true;
                    _startHoldPosition = MousePosition;

                    return ButtonState.JustDown;
                }

                if (justReleased)
                {
                    _isHolding = false;

                    var distance = _startHoldPosition.DistanceTo(MousePosition);

                    var holdTooLong = _holdTime >= Constants.Input.HoldDurationForClick;
                    var releasedTooFar = distance >= Constants.Input.MaxDistanceToConsiderClick;

                    return holdTooLong || releasedTooFar
                        ? ButtonState.JustUp
                        : ButtonState.Clicked;
                }

                if (_isHolding)
                {
                    _holdTime += deltaTime;
                    return ButtonState.Down;
                }

                return ButtonState.Up;
            }
        }
    }
}
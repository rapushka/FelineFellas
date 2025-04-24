using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class EmitInputSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<InputScope>> _inputs
            = GroupBuilder<InputScope>
                .With<PlayerInput>()
                .Build();

        private static IInputService InputService => ServiceLocator.Resolve<IInputService>();

        public void Execute()
        {
            foreach (var input in _inputs)
            {
                var lmb = InputService.LeftMouseButton;

                input
                    .Is<CursorJustDown>(lmb is ButtonState.JustDown)
                    .Is<CursorJustUp>(lmb is ButtonState.JustUp)
                    .Is<CursorJustClicked>(lmb is ButtonState.Clicked)
                    .Is<CursorHolding>(lmb is ButtonState.Down)
                    ;
            }
        }
    }
}
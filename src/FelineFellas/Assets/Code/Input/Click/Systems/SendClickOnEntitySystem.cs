using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class SendClickOnEntitySystem : IExecuteSystem
    {
        private readonly IGroup<Entity<InputScope>> _inputs
            = GroupBuilder<InputScope>
                .With<PlayerInput>()
                .And<CursorJustClicked>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _hoveredEntities
            = GroupBuilder<GameScope>
                .With<Hovered>()
                .Build();

        public void Execute()
        {
            foreach (var _ in _inputs)
            foreach (var hovered in _hoveredEntities)
            {
                hovered.Is<Clicked>(true);
            }
        }
    }
}
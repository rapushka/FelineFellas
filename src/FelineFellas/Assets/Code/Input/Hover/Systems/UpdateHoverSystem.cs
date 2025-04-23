using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UpdateHoverSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<InputScope>> _inputs
            = GroupBuilder<InputScope>
                .With<PlayerInput>()
                .And<WorldPosition>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _targets
            = GroupBuilder<GameScope>
                .With<Interactable>()
                .And<Collider>()
                .And<Sorting>()
                .Build();

        public void Execute()
        {
            foreach (var input in _inputs)
            {
                var mousePosition = input.Get<WorldPosition>().Value;
                Entity<GameScope> topmostTarget = null;

                foreach (var target in _targets)
                {
                    var collider = target.Get<Collider>().Value;
                    var mouseIsOverTarget = collider.OverlapPoint(mousePosition);

                    if (!mouseIsOverTarget)
                        continue;

                    if (topmostTarget is not null)
                    {
                        var targetSorting = target.Get<Sorting>().Value;
                        var topmostSorting = topmostTarget.Get<Sorting>().Value;

                        if (targetSorting <= topmostSorting)
                            continue;
                    }

                    topmostTarget = target;
                }

                topmostTarget?.Is<Hovered>(true);
            }
        }
    }
}
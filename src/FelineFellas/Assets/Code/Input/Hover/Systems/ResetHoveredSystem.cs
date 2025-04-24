using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class ResetHoveredSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _targets
            = GroupBuilder<GameScope>
                .With<Hovered>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(8);

        public void Execute()
        {
            foreach (var target in _targets.GetEntities(_buffer))
                target.Is<Hovered>(false);
        }
    }
}
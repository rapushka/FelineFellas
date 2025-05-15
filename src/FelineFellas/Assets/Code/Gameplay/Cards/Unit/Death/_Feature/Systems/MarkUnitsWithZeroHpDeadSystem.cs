using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class MarkUnitsWithZeroHpDeadSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _units
            = GroupBuilder<GameScope>
                .With<Health>()
                .Without<Dead>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(32);

        public void Execute()
        {
            foreach (var unit in _units.GetEntities(_buffer))
            {
                var isDead = unit.Get<Health>().Value <= 0;
                unit.Is<Dead>(isDead);
            }
        }
    }
}
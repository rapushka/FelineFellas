using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CleanupDroppedSystem : ICleanupSystem
    {
        private readonly IGroup<Entity<GameScope>> _entities
            = GroupBuilder<GameScope>
                .With<Dropped>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(8);

        public void Cleanup()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
                entity.Is<Dropped>(false);
        }
    }
}
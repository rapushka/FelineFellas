using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class EndEntityInitializationSystem : ICleanupSystem
    {
        private readonly IGroup<Entity<GameScope>> _entities
            = GroupBuilder<GameScope>
                .With<Initializing>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new();

        public void Cleanup()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
                entity.Is<Initializing>(false);
        }
    }
}
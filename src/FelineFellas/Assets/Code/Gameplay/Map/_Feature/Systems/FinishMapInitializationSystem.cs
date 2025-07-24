using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class FinishMapInitializationSystem : ICleanupSystem
    {
        private readonly IGroup<Entity<GameScope>> _maps
            = GroupBuilder<GameScope>
                .With<Map>()
                .And<InitializingMap>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new();

        public void Cleanup()
        {
            foreach (var map in _maps.GetEntities(_buffer))
                map.Is<InitializingMap>(false);
        }
    }
}
using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CleanupUsedSystem : ICleanupSystem
    {
        private readonly IGroup<Entity<GameScope>> _usedCards
            = GroupBuilder<GameScope>
                .With<Used>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(16);

        public void Cleanup()
        {
            foreach (var usedCard in _usedCards.GetEntities(_buffer))
            {
                usedCard.Is<Used>(false);
            }
        }
    }
}
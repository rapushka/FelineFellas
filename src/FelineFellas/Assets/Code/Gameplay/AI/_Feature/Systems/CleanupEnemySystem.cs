using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public class CleanupEnemySystem : ICleanupSystem
    {
        private readonly IGroup<Entity<GameScope>> _enemies
            = GroupBuilder<GameScope>
                .With<ActiveEnemyActor>()
                .And<TryPlayCard>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(4);

        public void Cleanup()
        {
            foreach (var enemy in _enemies.GetEntities(_buffer))
            {
                enemy
                    .Is<TryPlayCard>(false)
                    .RemoveSafely<CardToPlay>()
                    .RemoveSafely<WillPlayActionCard>()
                    ;
            }
        }
    }
}
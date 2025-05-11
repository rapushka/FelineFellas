using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public class DestroyEntitiesSystem : ICleanupSystem
    {
        private readonly IGroup<Entity<GameScope>> _entities
            = GroupBuilder<GameScope>
                .With<Destroy>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(64);

        public void Cleanup()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                if (entity.TryGet<View, GameEntityBehaviour>(out var view))
                {
                    view.Unregister();
                    view.DestroyObject();
                }

                entity.Destroy();
            }
        }
    }
}
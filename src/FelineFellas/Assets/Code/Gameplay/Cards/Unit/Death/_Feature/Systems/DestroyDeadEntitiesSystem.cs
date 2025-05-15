using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class DestroyDeadEntitiesSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _entities
            = GroupBuilder<GameScope>
                .With<Dead>()
                .Build();

        public void Execute()
        {
            foreach (var entity in _entities)
                entity.Is<Destroy>(true);
        }
    }
}
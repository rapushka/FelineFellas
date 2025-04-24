using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class ResetSortingOrderSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _entities
            = GroupBuilder<GameScope>
                .With<SpriteSortingGroup>()
                .Build();

        public void Execute()
        {
            foreach (var entity in _entities)
            {
                var sortingGroup = entity.Get<SpriteSortingGroup>().Value;
                entity.Set<SpriteSortingIndex, int>((int)sortingGroup);
            }
        }
    }
}
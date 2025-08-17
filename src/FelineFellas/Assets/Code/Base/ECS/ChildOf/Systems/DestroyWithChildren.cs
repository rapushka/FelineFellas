using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class DestroyWithChildrenSystem : ICleanupSystem
    {
        private readonly IGroup<Entity<GameScope>> _destroyedParents
            = GroupBuilder<GameScope>
                .With<ID>()
                .And<Destroy>()
                .Build();

        private static EntityIndex<GameScope, ChildOf, EntityID> Index
            => Contexts.Instance.Get<GameScope>().GetIndex<ChildOf, EntityID>();

        private readonly List<Entity<GameScope>> _buffer = new(16);

        public void Cleanup()
        {
            foreach (var parent in _destroyedParents.GetEntities(_buffer))
                DestroyChildren(parent);
        }

        private void DestroyChildren(Entity<GameScope> parent)
        {
            foreach (var child in Index.GetEntities(parent.ID()))
            {
                child.Is<Destroy>(true);
                DestroyChildren(child);
            }
        }
    }
}
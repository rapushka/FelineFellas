using Entitas.Generic;

namespace FelineFellas
{
    public static class EntityIDExtension
    {
        private static PrimaryEntityIndex<GameScope, ID, EntityID> Index
            => Contexts.Instance.Get<GameScope>().GetPrimaryIndex<ID, EntityID>();

        public static bool TryGetEntity(this EntityID @this, out Entity<GameScope> entity)
            => Index.TryGetEntity(@this, out entity);
    }
}
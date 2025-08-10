using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Generic;
using GameEntity = Entitas.Generic.Entity<FelineFellas.GameScope>;

namespace FelineFellas
{
    public static class EntitiesGroupFindExtensions
    {
        public static GameEntity FindWithMinValue<TComponent>(this IGroup<GameEntity> @this)
            where TComponent : ValueComponent<int>, IInScope<GameScope>, new()
            => @this.FindWithMin((e) => e.Get<TComponent, int>());

        public static GameEntity FindWithMin(this IGroup<GameEntity> @this, Func<GameEntity, int> selector)
        {
            var minValue = 0;
            GameEntity entityWithMin = null;

            foreach (var entity in @this)
            {
                var value = selector(entity);

                if (entityWithMin == null
                    || minValue > value)
                {
                    entityWithMin = entity;
                    minValue = value;
                }
            }

            return entityWithMin;
        }

        public static GameEntity FindWithMin(this IEnumerable<GameEntity> @this, Func<GameEntity, int> selector)
        {
            var minValue = 0;
            GameEntity entityWithMin = null;

            foreach (var entity in @this)
            {
                var value = selector(entity);

                if (entityWithMin == null
                    || minValue > value)
                {
                    entityWithMin = entity;
                    minValue = value;
                }
            }

            return entityWithMin;
        }
    }
}
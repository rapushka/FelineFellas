using System.Collections.Generic;
using Entitas;
using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public sealed class AnimateScaleSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _entities
            = GroupBuilder<GameScope>
                .With<TargetScale>()
                .And<Scale>()
                .And<AnimationsSpeed>()
                .Build();

        private static ITimeService TimeService => ServiceLocator.Resolve<ITimeService>();

        private readonly List<Entity<GameScope>> _buffer = new(16);

        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                var targetScale = entity.Get<TargetScale>().Value;
                var currentScale = entity.Get<Scale>().Value;
                var scalingSpeed = entity.Get<AnimationsSpeed>().Value;

                var direction = targetScale - currentScale;

                var delta = scalingSpeed * TimeService.AnimationDelta;

                if (Mathf.Abs(direction) < delta)
                {
                    entity
                        .Set<Scale, float>(targetScale)
                        .Remove<TargetScale>()
                        ;
                    continue;
                }

                direction = Mathf.Sign(direction);
                entity.Set<Scale, float>(currentScale + direction * delta);
            }
        }
    }
}
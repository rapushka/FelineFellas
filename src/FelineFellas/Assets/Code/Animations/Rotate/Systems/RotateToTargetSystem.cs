using System.Collections.Generic;
using Entitas;
using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public sealed class RotateToTargetSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _entities
            = GroupBuilder<GameScope>
                .With<TargetRotation>()
                .And<Rotation>()
                .And<AnimationsSpeed>()
                .Build();

        private static ITimeService TimeService => ServiceLocator.Resolve<ITimeService>();

        private readonly List<Entity<GameScope>> _buffer = new(64);

        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                var targetRotation = entity.Get<TargetRotation>().Value;
                var currentRotation = entity.Get<Rotation>().Value;
                var rotationSpeed = entity.Get<AnimationsSpeed>().Value;

                var direction = targetRotation - currentRotation;

                direction += direction > 180 ? -360
                    : direction < -180       ? 360
                                               : 0;

                var deltaRotation = rotationSpeed * TimeService.AnimationDelta;

                if (Mathf.Abs(direction) < deltaRotation)
                {
                    entity
                        .Set<Rotation, float>(targetRotation)
                        .Remove<TargetRotation>()
                        ;
                    continue;
                }

                direction = Mathf.Sign(direction);
                entity.Set<Rotation, float>(currentRotation + direction * deltaRotation);
            }
        }
    }
}
using System.Collections.Generic;
using Entitas;
using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public sealed class MoveToPositionSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _entities
            = GroupBuilder<GameScope>
                .With<TargetPosition>()
                .And<WorldPosition>()
                .And<MovementSpeed>()
                .Build();

        private static ITimeService TimeService => ServiceLocator.Resolve<ITimeService>();

        private readonly List<Entity<GameScope>> _buffer = new(64);

        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                var target = entity.Get<TargetPosition>().Value;
                var position = entity.Get<WorldPosition>().Value;
                var speed = entity.Get<MovementSpeed>().Value;

                var scaledSpeed = speed * TimeService.AnimationDelta;
                var direction = (target - position).normalized;

                var distance = position.DistanceTo(target);

                if (distance <= scaledSpeed)
                {
                    entity
                        .Set<WorldPosition, Vector2>(target)
                        .Remove<TargetPosition>();
                    continue;
                }

                var movement = direction * scaledSpeed;
                entity.Set<WorldPosition, Vector2>(position + movement);
            }
        }
    }
}
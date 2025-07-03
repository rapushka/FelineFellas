using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class SelectSelfForTargetObjectSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _abilities
            = GroupBuilder<GameScope>
                .With<AbilityUse>()
                .And<TargetObjectAsSelf>()
                .And<TargetSubject>()
                .Build();

        public void Execute()
        {
            foreach (var ability in _abilities)
            {
                var sender = ability.Get<TargetObject>().Value;
                ability.Set<TargetObject, EntityID>(sender);
            }
        }
    }
}
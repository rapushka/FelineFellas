using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class DestroyAbilityUsagesSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _abilities
            = GroupBuilder<GameScope>
                .With<AbilityUse>()
                .Build();

        public void Execute()
        {
            foreach (var ability in _abilities)
                ability.Is<Destroy>(true);
        }
    }
}
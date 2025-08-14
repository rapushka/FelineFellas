using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class DefeatEnemyLeaderOnDeathSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _deadLeaders
            = GroupBuilder<GameScope>
                .With<Leader>()
                .And<EnemyCard>()
                .And<Dead>()
                .Build();

        public void Execute()
        {
            foreach (var leader in _deadLeaders)
                leader.Is<Defeated>(true);
        }
    }
}
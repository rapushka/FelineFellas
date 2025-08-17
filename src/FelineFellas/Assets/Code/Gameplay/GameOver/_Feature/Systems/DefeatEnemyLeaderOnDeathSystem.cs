using System.Collections.Generic;
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
                .Without<Defeated>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(2);

        public void Execute()
        {
            foreach (var leader in _deadLeaders.GetEntities(_buffer))
            {
                leader.Is<Defeated>(true);

                var stage = leader.Get<EnemyLeadOnStage>().Value.GetEntity();
                stage.Is<CompletedStage>(true);

                CreateEntity.Empty().Add<StageCompletedEvent>();
            }
        }
    }
}
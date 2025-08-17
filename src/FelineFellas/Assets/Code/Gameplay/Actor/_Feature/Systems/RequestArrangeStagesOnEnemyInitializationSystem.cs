using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class RequestArrangeStagesOnEnemyInitializationSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _enemies
            = GroupBuilder<GameScope>
                .With<LeadOnStage>()
                .And<Initializing>()
                .Without<PlayerCard>()
                .Build();

        public void Execute()
        {
            if (!_enemies.Any())
                return;

            CreateEntity.Empty()
                .Add<ArrangeStagesEvent>()
                ;
        }
    }
}
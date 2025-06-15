using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CompleteStageLoadingSystem : ICleanupSystem
    {
        private readonly IGroup<Entity<GameScope>> _stages
            = GroupBuilder<GameScope>
                .With<Stage>()
                .And<EnteringStage>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(4);

        public void Cleanup()
        {
            foreach (var stage in _stages.GetEntities(_buffer))
                stage.Is<EnteringStage>(false);
        }
    }
}
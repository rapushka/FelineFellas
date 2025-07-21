using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CreateStageOnFightStartSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _event
            = GroupBuilder<GameScope>
                .With<StartFight>()
                .Build();

        public void Execute()
        {
            foreach (var _ in _event)
            {
                CreateEntity.Empty()
                    .Add<Name, string>("stage")
                    .Is<Stage>(true)
                    .Is<EnteringStage>(true)
                    ;
            }
        }
    }
}
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CreateStagesOnMapCreatedSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _maps
            = GroupBuilder<GameScope>
                .With<Map>()
                .And<Initializing>()
                .Build();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static IStageFactory StageFactory => ServiceLocator.Resolve<IStageFactory>();

        public void Execute()
        {
            foreach (var map in _maps)
            {
                var usualStageCount = GameConfig.Map.NumberOfUsualEnemies;
                var mapID = map.ID();

                for (var i = 0; i < usualStageCount; i++)
                    StageFactory.Create(i + 1, mapID);

                StageFactory.Create(usualStageCount + 1, mapID)
                    .Add<FinalStage>();
            }
        }
    }
}
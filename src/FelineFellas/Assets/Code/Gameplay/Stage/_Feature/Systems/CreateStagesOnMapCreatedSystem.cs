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
                for (var i = 0; i < GameConfig.Map.NumberOfUsualEnemies; i++)
                    StageFactory.Create(i + 1, map.ID());
            }
        }
    }
}
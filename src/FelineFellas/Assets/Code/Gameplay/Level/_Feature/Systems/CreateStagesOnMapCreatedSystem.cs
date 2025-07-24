using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CreateStagesOnMapCreatedSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _maps
            = GroupBuilder<GameScope>
                .With<Map>()
                .And<InitializingMap>()
                .Build();

        public void Execute()
        {
            foreach (var map in _maps)
            {
                CreateEntity.Empty()
                    .Add<Name, string>("stage")
                    .Is<Stage>(true)
                    .SetParent(map)
                    ;
            }
        }
    }
}
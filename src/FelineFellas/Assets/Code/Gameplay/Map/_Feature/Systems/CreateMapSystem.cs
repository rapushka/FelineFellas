using Entitas;

namespace FelineFellas
{
    public sealed class CreateMapSystem : IInitializeSystem
    {
        public void Initialize()
        {
            CreateEntity.Empty()
                .Add<Name, string>("map")
                .Add<Map>()
                .Add<InitializingMap>()
                ;
        }
    }
}
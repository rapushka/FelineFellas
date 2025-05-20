using Entitas;

namespace FelineFellas
{
    public sealed class CreateLevelSystem : IInitializeSystem
    {
        public void Initialize()
        {
            CreateEntity.Empty()
                .Add<Name, string>("level")
                .Is<Level>(true)
                ;
        }
    }
}
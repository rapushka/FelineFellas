using Entitas;

namespace FelineFellas
{
    public sealed class CreateStageSystem : IInitializeSystem
    {
        public void Initialize()
        {
            CreateEntity.Empty()
                .Add<Name, string>("stage")
                .Is<Stage>(true)
                .Is<EnteringStage>(true)
                ;
        }
    }
}
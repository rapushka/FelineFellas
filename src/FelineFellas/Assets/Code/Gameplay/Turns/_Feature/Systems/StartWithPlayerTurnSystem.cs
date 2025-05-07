using Entitas;

namespace FelineFellas
{
    public sealed class StartWithPlayerTurnSystem : IInitializeSystem
    {
        public void Initialize()
        {
            CreateEntity.OneFrame()
                .Add<StartTurnEvent>();
        }
    }
}
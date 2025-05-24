using Entitas;

namespace FelineFellas
{
    public sealed class CreateTurnMediatorSystem : IInitializeSystem
    {
        public void Initialize()
        {
            CreateEntity.Empty()
                .Add<Name, string>("Turn Mediator")
                .Add<TurnMediator>()
                .Add<OnPlayerTurnStartedState>()
                ;
        }
    }
}
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class OnStartFightHideMapUiSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<StartFightEvent>()
                .Build();

        private static IUiMediator UiMediator => ServiceLocator.Resolve<IUiMediator>();

        private static GameplayHUD HUD => UiMediator.Pages.GetCurrent<GameplayHUD>();

        public void Execute()
        {
            foreach (var _ in _events)
                HUD.MapUi.Hide();
        }
    }
}
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class ShowMapViewOnMapInitializedSystem : ICleanupSystem
    {
        private readonly IGroup<Entity<GameScope>> _maps
            = GroupBuilder<GameScope>
                .With<Map>()
                .And<Initializing>()
                .Build();

        private static IUiMediator UiMediator => ServiceLocator.Resolve<IUiMediator>();

        private static GameplayHUD HUD => UiMediator.Pages.GetCurrent<GameplayHUD>();

        public void Cleanup()
        {
            foreach (var _ in _maps)
                HUD.MapUi.Show();
        }
    }
}
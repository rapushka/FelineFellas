using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UpdateMoneyViewSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _wallets
            = GroupBuilder<GameScope>
                .With<Player>()
                .And<Money>()
                .Build();

        private static IUiMediator UiMediator => ServiceLocator.Resolve<IUiMediator>();

        private static GameplayHUD HUD => UiMediator.Pages.GetCurrent<GameplayHUD>();

        public void Execute()
        {
            foreach (var wallet in _wallets)
            {
                HUD.MoneyView.Value = wallet.Get<Money>().Value;
            }
        }
    }
}
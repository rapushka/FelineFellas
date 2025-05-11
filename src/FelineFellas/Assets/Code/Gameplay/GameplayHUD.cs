using UnityEngine;
using UnityEngine.UI;

namespace FelineFellas
{
    public class GameplayHUD : BasePage
    {
        [SerializeField] private Button _endTurnButton;

        private static IUiMediator UiMediator => ServiceLocator.Resolve<IUiMediator>();

        protected override void Initialize()
        {
            _endTurnButton.onClick.AddListener(EndTurn);
        }

        protected override void Dispose()
        {
            _endTurnButton.onClick.RemoveListener(EndTurn);
        }

        private void EndTurn()
        {
            UiMediator.EndTurn();
        }
    }
}
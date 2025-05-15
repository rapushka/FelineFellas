using UnityEngine;
using UnityEngine.UI;

namespace FelineFellas
{
    public class GameplayHUD : BasePage
    {
        [SerializeField] private Button _endTurnButton;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private MoneyView _moneyView;

        private static IUiMediator UiMediator => ServiceLocator.Resolve<IUiMediator>();

        public MoneyView MoneyView => _moneyView;

        protected override void Initialize()
        {
            _endTurnButton.onClick.AddListener(EndTurn);
            _pauseButton.onClick.AddListener(BackToMainMenu);
        }

        protected override void Dispose()
        {
            _endTurnButton.onClick.RemoveListener(EndTurn);
            _pauseButton.onClick.RemoveListener(BackToMainMenu);
        }

        private void EndTurn()
        {
            UiMediator.EndTurn();
        }

        private void BackToMainMenu()
        {
            UiMediator.EndGame();
        }
    }
}
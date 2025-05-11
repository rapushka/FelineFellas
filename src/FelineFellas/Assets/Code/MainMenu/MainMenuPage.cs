using UnityEngine;
using UnityEngine.UI;

namespace FelineFellas
{
    public class MainMenuPage : BasePage
    {
        [SerializeField] private Button _startGameButton;

        private static IUiMediator UiMediator => ServiceLocator.Resolve<IUiMediator>();

        protected override void Initialize()
        {
            _startGameButton.onClick.AddListener(StartGame);
        }

        protected override void Dispose()
        {
            _startGameButton.onClick.RemoveListener(StartGame);
        }

        private void StartGame()
        {
            UiMediator.StartGame();
        }
    }
}
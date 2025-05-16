using UnityEngine;
using UnityEngine.UI;

namespace FelineFellas
{
    public class GameOverPage : BasePage
    {
        [SerializeField] private Button _toMainMenuButton;

        private static IUiMediator UiMediator => ServiceLocator.Resolve<IUiMediator>();

        protected override void Initialize()
        {
            _toMainMenuButton.onClick.AddListener(OnToMainMenuButtonCLicked);
        }

        protected override void Dispose()
        {
            _toMainMenuButton.onClick.RemoveListener(OnToMainMenuButtonCLicked);
        }

        private void OnToMainMenuButtonCLicked()
        {
            UiMediator.ToMainMenu();
        }
    }
}
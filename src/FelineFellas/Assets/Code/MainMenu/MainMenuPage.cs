using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FelineFellas
{
    public class MainMenuPage : BasePage
    {
        [SerializeField] private Button _startGameButtonPrefab;
        [SerializeField] private Transform _root;

        private readonly List<Button> _buttons = new();

        private static IUiMediator UiMediator => ServiceLocator.Resolve<IUiMediator>();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        protected override void Initialize()
        {
            foreach (var mode in GameConfig.GameModes.GameModes)
            {
                var button = Instantiate(_startGameButtonPrefab, _root);
                button.onClick.AddListener(() => StartGame(mode));
                button.GetComponentInChildren<TMP_Text>().text = $"Play – {mode.Name}";
                button.gameObject.SetActive(true);

                _buttons.Add(button);
            }
        }

        protected override void Dispose() => _buttons.DestroyAllObjects();

        private void StartGame(IGameMode gameMode)
        {
            UiMediator.StartGame(gameMode);
        }
    }
}
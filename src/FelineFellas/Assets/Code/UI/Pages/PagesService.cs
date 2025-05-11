using UnityEngine;

namespace FelineFellas
{
    public interface IPagesService : IService
    {
        void OpenMainMenu();
        void OpenGameplay();

        void HideAll();
    }

    public class PagesService : IPagesService, IInitializableService
    {
        private MainMenuPage _mainMenu;
        private GameplayHUD _gameplayHud;

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static IUIService UIService => ServiceLocator.Resolve<IUIService>();

        void IInitializableService.Initialize()
        {
            _gameplayHud = Object.Instantiate(GameConfig.UI.HUDPrefab, UIService.CanvasRoot);
            _mainMenu = Object.Instantiate(GameConfig.UI.MainMenuPrefab, UIService.CanvasRoot);

            HideAll();
        }

        public void OpenMainMenu()
        {
            HideAll();
            _mainMenu.Show();
        }

        public void OpenGameplay()
        {
            HideAll();
            _gameplayHud.Show();
        }

        public void HideAll()
        {
            _gameplayHud.Hide();
            _mainMenu.Hide();
        }
    }
}
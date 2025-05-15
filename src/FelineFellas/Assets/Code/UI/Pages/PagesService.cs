using UnityEngine;

namespace FelineFellas
{
    public interface IPagesService : IService
    {
        TPage GetCurrent<TPage>() where TPage : BasePage;

        void OpenMainMenu();
        void OpenGameplay();

        void HideAll();
    }

    public class PagesService : IPagesService, IInitializableService
    {
        private MainMenuPage _mainMenu;
        private GameplayHUD _gameplayHud;

        private BasePage _currentPage;

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static IUIService UIService => ServiceLocator.Resolve<IUIService>();

        void IInitializableService.Initialize()
        {
            _gameplayHud = Object.Instantiate(GameConfig.UI.HUDPrefab, UIService.CanvasRoot);
            _mainMenu = Object.Instantiate(GameConfig.UI.MainMenuPrefab, UIService.CanvasRoot);

            HideAll();
        }

        public TPage GetCurrent<TPage>() where TPage : BasePage => (TPage)_currentPage;

        public void OpenMainMenu()
        {
            HideAll();
            _mainMenu.Show();
            _currentPage = _mainMenu;
        }

        public void OpenGameplay()
        {
            HideAll();
            _gameplayHud.Show();
            _currentPage = _gameplayHud;
        }

        public void HideAll()
        {
            _gameplayHud.Hide();
            _mainMenu.Hide();

            _currentPage = null;
        }
    }
}
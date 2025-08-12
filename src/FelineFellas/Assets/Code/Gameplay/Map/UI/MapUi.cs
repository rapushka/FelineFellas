using UnityEngine;
using UnityEngine.UI;

namespace FelineFellas
{
    public class MapUi : MonoBehaviour
    {
        [SerializeField] private Button _fightButton;
        [SerializeField] private RectTransform _currentStageViewRoot;
        [SerializeField] private GameObject _root;

        private static ICamerasService CamerasService => ServiceLocator.Resolve<ICamerasService>();

        public void Initialize()
        {
            _fightButton.onClick.AddListener(OnFightButtonClicked);
        }

        public void Dispose()
        {
            _fightButton.onClick.RemoveListener(OnFightButtonClicked);
        }

        public void Show()
        {
            var nextEnemyLead = MapUtils.GetNextEnemyLead();

            var enemyWorldPosition = nextEnemyLead.WorldPosition();
            var enemyScreenPosition = CamerasService.WorldToScreen(enemyWorldPosition);

            _currentStageViewRoot.anchoredPosition = enemyScreenPosition;

            _root.SetActive(true);
        }

        public void Hide()
        {
            _root.SetActive(false);
        }

        private void OnFightButtonClicked()
        {
            CreateEntity.Empty()
                .Add<StartFightWithNextEnemyLead>()
                ;
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace FelineFellas
{
    public class MapUi : MonoBehaviour
    {
        [SerializeField] private Button _fightButton;
        [SerializeField] private GameObject _root;

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
            // TODO: get the position of current enemy and place Fight Button on it

            _root.SetActive(true);
        }

        public void Hide()
        {
            _root.SetActive(false);
        }

        private void OnFightButtonClicked() { }
    }
}
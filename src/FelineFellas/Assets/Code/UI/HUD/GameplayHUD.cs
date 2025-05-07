using UnityEngine;
using UnityEngine.UI;

namespace FelineFellas
{
    public class GameplayHUD : MonoBehaviour
    {
        [SerializeField] private Button _endTurnButton;

        private void OnEnable()
        {
            _endTurnButton.onClick.AddListener(EndTurn);
        }

        private void OnDisable()
        {
            _endTurnButton.onClick.RemoveListener(EndTurn);
        }

        private void EndTurn()
        {
            CreateEntity.OneFrame()
                .Add<EndTurnEvent>()
                ;
        }
    }
}
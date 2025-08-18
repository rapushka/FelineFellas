using DG.Tweening;
using TMPro;
using UnityEngine;

namespace FelineFellas
{
    public class TurnAnnounceView : MonoBehaviour
    {
        [SerializeField] private GameObject _labelRoot;
        [SerializeField] private TMP_Text _label;

        private Tween _tween;

        public void Initialize() => _labelRoot.transform.SetScale(0f);

        public void OnPlayerTurnStarted() => Show("Player's Turn");

        public void OnEnemyTurnStarted() => Show("Enemy's Turn");

        private void Show(string text)
        {
            _tween?.Kill();
            _labelRoot.transform.SetScale(0f);

            _label.text = text;

            _tween = DOTween.Sequence()
                .Append(
                    _labelRoot.transform.DOScale(1f, 0.5f)
                        .SetEase(Ease.OutBack)
                )
                .AppendInterval(0.5f)
                .Append(
                    _labelRoot.transform.DOScale(0f, 0.25f)
                        .SetEase(Ease.InOutSine)
                );
        }
    }
}
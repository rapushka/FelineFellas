using UnityEngine;

namespace FelineFellas
{
    public class SpriteProgressBar : MonoBehaviour
    {
        [SerializeField] private Transform _background;
        [SerializeField] private Transform _fill;

        public void DoNormalizedValue(float value)
        {
            var bgScale = _background.localScale;
            var scale = _fill.localScale;
            scale.x = bgScale.x * value.Clamp();
            _fill.localScale = scale;
        }
    }
}
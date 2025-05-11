using UnityEngine;

namespace FelineFellas
{
    public abstract class BasePage : MonoBehaviour
    {
        private void OnEnable() => Initialize();

        private void OnDisable() => Dispose();

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);

        protected abstract void Initialize();

        protected virtual void Dispose() { }
    }
}
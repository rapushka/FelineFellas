using UnityEngine;

namespace FelineFellas
{
    public abstract class BasePage : MonoBehaviour
    {
        private void Awake() => Initialize();

        private void OnDestroy() => Dispose();

        public virtual void Show() => gameObject.SetActive(true);

        public virtual void Hide() => gameObject.SetActive(false);

        protected abstract void Initialize();

        protected virtual void Dispose() { }
    }
}
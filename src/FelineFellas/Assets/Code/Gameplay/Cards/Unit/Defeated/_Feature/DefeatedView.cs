using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public class DefeatedView : BaseListener<GameScope, Defeated>
    {
        [SerializeField] private GameObject _viewRoot;

        protected override void OnRegistered(Entity<GameScope> entity) => UpdateView(entity);

        public override void OnValueChanged(Entity<GameScope> entity, Defeated component) => UpdateView(entity);

        private void UpdateView(Entity<GameScope> entity)
            => _viewRoot.SetActive(entity.Is<Defeated>());
    }
}
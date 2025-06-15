using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public class SortingGroupView : BaseListener<GameScope, RenderOrderIndex>
    {
        [SerializeField] private UnityEngine.Rendering.SortingGroup _sortingGroup;

        public override void OnValueChanged(Entity<GameScope> entity, RenderOrderIndex component)
            => _sortingGroup.sortingOrder = component.Value;
    }
}
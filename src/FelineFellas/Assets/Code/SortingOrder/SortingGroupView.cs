using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public class SortingGroupView : BaseListener<GameScope, SpriteSortingIndex>
    {
        [SerializeField] private UnityEngine.Rendering.SortingGroup _sortingGroup;

        public override void OnValueChanged(Entity<GameScope> entity, SpriteSortingIndex component)
            => _sortingGroup.sortingOrder = component.Value;
    }
}
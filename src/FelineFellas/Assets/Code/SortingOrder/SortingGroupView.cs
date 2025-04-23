using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public class SortingGroupView : BaseListener<GameScope, Sorting>
    {
        [SerializeField] private UnityEngine.Rendering.SortingGroup _sortingGroup;

        public override void OnValueChanged(Entity<GameScope> entity, Sorting component)
            => _sortingGroup.sortingOrder = (int)component.Value;
    }
}
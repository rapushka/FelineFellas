using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public class FieldConfig
    {
        /// Count of Cells in a Row
        [field: SerializeField] public int RowWidth { get; private set; }

        [field: SerializeField] public Size FieldSize { get; private set; } // TODO: REMOVE ME

        [field: SerializeField] public ViewConfig View { get; private set; }

        [Serializable]
        public class ViewConfig
        {
            [field: SerializeField] public GameEntityBehaviour FieldPrefab { get; private set; }
            [field: SerializeField] public GameEntityBehaviour RowPrefab   { get; private set; }
            [field: SerializeField] public GameEntityBehaviour CellPrefab  { get; private set; }

            [field: SerializeField] public Vector2 FieldCenter { get; private set; } = new(0f, 1f);

            /// Between Cells
            [field: SerializeField] public Vector2 Spacings { get; private set; }

            [field: SerializeField] public float DistanceBetweenRows { get; private set; }
        }
    }
}
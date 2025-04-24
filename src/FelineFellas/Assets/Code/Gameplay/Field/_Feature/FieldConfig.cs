using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public class FieldConfig
    {
        [field: SerializeField] public Size FieldSize { get; private set; }

        [field: SerializeField] public ViewConfig View { get; private set; }

        [Serializable]
        public class ViewConfig
        {
            [field: SerializeField] public GameEntityBehaviour FieldPrefab { get; private set; }
            [field: SerializeField] public GameEntityBehaviour CellPrefab  { get; private set; }

            [field: SerializeField] public Vector2 FieldCenter { get; private set; } = new(0f, 1f);
            [field: SerializeField] public Vector2 Spacings    { get; private set; }
        }
    }
}
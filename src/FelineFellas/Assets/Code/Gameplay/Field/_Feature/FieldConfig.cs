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
            [field: SerializeField] public GameEntityBehaviour ViewPrefab { get; private set; }

            [field: SerializeField] public Vector2 Spacings { get; private set; }
        }
    }
}
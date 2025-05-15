using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public class MoneyConfig
    {
        [field: SerializeField] public int MoneyOnStart { get; private set; }

        [field: Header("Shop")]
        [field: SerializeField] public ShopViewConfig ShopView { get; private set; }

        [Serializable]
        public class ShopViewConfig
        {
            [field: SerializeField] public Vector2 ShopSpawnPosition { get; private set; } = new(7.46f, 0f);

            [field: SerializeField] public GameEntityBehaviour ShopPrefab { get; private set; }
        }
    }
}
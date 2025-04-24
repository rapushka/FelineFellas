using System;
using System.Linq;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public class CardsConfig
    {
        [SerializeField] private CardConfig[] _configs;

        [field: SerializeField] public CardIDRef[] StartPlayerDeck { get; private set; }
        [field: SerializeField] public int         HandSize        { get; private set; }

        [field: SerializeField] public ViewConfig View { get; private set; }

        public CardConfig GetConfig(CardIDRef id) => _configs.Single(c => c.ID == id);

        [Serializable]
        public class ViewConfig
        {
            [field: SerializeField] public GameEntityBehaviour ViewPrefab          { get; private set; }
            [field: SerializeField] public Vector2             DeckSpawnPosition   { get; private set; }
            [field: SerializeField] public float               CardAnimationsSpeed { get; private set; } = 10f;

            [field: Header("Hand")]
            [field: SerializeField] public float MaxCardAngle { get; private set; } = 15f;

            [field: SerializeField] public float CardSpacing    { get; private set; } = 1.5f;
            [field: SerializeField] public float VerticalOffset { get; private set; } = 0.5f;
            [field: SerializeField] public float HandRadius     { get; private set; } = 5f;

            [field: SerializeField] public Vector2 HandCenter { get; private set; } = new(0f, -4f);
        }
    }
}
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

        [field: SerializeField] public ViewConfig View { get; private set; }

        public CardConfig GetConfig(CardIDRef id) => _configs.Single(c => c.ID == id);

        [Serializable]
        public class ViewConfig
        {
            [field: SerializeField] public GameEntityBehaviour ViewPrefab        { get; private set; }
            [field: SerializeField] public Vector2             DeckSpawnPosition { get; private set; }
        }
    }
}
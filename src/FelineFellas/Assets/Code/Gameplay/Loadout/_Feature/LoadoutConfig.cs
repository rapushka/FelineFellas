using System;
using UnityEngine;

namespace FelineFellas
{
    [CreateAssetMenu(menuName = "375/FelineFellas/Loadout", order = 100)]
    public class LoadoutConfig : ScriptableObject
    {
        [field: SerializeField] public int         HandSize     { get; private set; }
        [field: SerializeField] public CardEntry[] Deck         { get; private set; }
        [field: SerializeField] public Placement[] UnitsOnField { get; private set; }

        [Serializable]
        public class Placement
        {
            [field: SerializeField] public CardIDRef   UnitID      { get; private set; }
            [field: SerializeField] public Coordinates Coordinates { get; private set; }
        }

        [Serializable]
        public class CardEntry
        {
            [field: SerializeField] public CardIDRef CardID { get; private set; }
            [field: SerializeField] public int       Count  { get; private set; }

            public void Deconstruct(out CardIDRef cardID, out int count)
            {
                cardID = CardID;
                count = Count;
            }
        }
    }
}
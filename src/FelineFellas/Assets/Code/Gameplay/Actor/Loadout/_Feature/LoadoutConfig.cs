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

            public void Deconstruct(out CardIDRef unitID, out Coordinates coordinates)
            {
                unitID = UnitID;
                coordinates = Coordinates;
            }
        }
    }
}
using System;
using UnityEngine;

namespace FelineFellas
{
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
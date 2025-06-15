using UnityEngine;

namespace FelineFellas
{
    [CreateAssetMenu(menuName = "375/FelineFellas/Loadout", order = 100)]
    public class LoadoutConfig : ScriptableObject
    {
        [field: SerializeField] public int         HandSize { get; private set; }
        [field: SerializeField] public CardEntry[] Deck     { get; private set; }
        [field: SerializeField] public CardIDRef   Lead     { get; private set; }
    }
}
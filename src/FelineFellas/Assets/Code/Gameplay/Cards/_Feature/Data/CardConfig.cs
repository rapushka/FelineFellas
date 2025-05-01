using UnityEngine;

namespace FelineFellas
{
    [CreateAssetMenu(menuName = "375/FelineFellas/Card", order = 100)]
    public class CardConfig : ScriptableObject
    {
        [field: SerializeField] public CardIDRef ID           { get; private set; }
        [field: SerializeField] public bool      IsGlobalCard { get; private set; }

        [field: NaughtyAttributes.BoxGroup("View")]
        [field: SerializeField] public string Title { get; private set; }

        [field: NaughtyAttributes.BoxGroup("View")]
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}
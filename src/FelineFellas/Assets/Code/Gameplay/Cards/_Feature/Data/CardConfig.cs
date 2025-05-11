using System;
using UnityEngine;

namespace FelineFellas
{
    [CreateAssetMenu(menuName = "375/FelineFellas/Card", order = 100)]
    public class CardConfig : ScriptableObject
    {
        [field: SerializeField] public CardIDRef ID    { get; private set; }
        [field: SerializeField] public UsageType Usage { get; private set; }

        [field: NaughtyAttributes.BoxGroup("View")]
        [field: SerializeField] public string Title { get; private set; }

        [field: NaughtyAttributes.BoxGroup("View")]
        [field: SerializeField] public Sprite Icon { get; private set; }

        [Serializable]
        [JetBrains.Annotations.UsedImplicitly]
        public enum UsageType
        {
            Unknown = 0,
            Global = 1,
            Unit = 2,
            Action = 3,
        }
    }
}
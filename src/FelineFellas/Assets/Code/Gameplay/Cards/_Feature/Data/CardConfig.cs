using System;
using UnityEngine;
using Naughty = NaughtyAttributes;

namespace FelineFellas
{
    [CreateAssetMenu(menuName = "375/FelineFellas/Card", order = 100)]
    public class CardConfig : ScriptableObject
    {
        [field: SerializeField] public CardIDRef ID    { get; private set; }
        [field: SerializeField] public UsageType Usage { get; private set; }

        [field: SerializeField] public int Price { get; private set; }

        [field: Naughty.BoxGroup("View")]
        [field: SerializeField] public string Title { get; private set; }

        [field: Naughty.BoxGroup("View")]
        [field: SerializeField] public Sprite Icon { get; private set; }

        [field: Naughty.BoxGroup("Action")]
        [field: Naughty.ShowIf(nameof(Usage), UsageType.Action)]
        [field: Naughty.HorizontalLine(color: Naughty.EColor.Yellow)]
        [field: SerializeField] public ActionCardConfig ActionCardConfig { get; private set; }

        [field: Naughty.BoxGroup("Unit")]
        [field: Naughty.ShowIf(nameof(Usage), UsageType.Unit)]
        [field: Naughty.HorizontalLine(color: Naughty.EColor.Blue)]
        [field: SerializeField] public UnitCardConfig UnitCardConfig { get; private set; }

        // Manually Color Coded For Editor
        [Serializable]
        [JetBrains.Annotations.UsedImplicitly]
        public enum UsageType
        {
            Unknown = 0,
            Global = 1, // Green
            Unit = 2,   // Blue
            Action = 3, // Yellow
        }
    }
}
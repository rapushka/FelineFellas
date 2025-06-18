using System;
using UnityEngine;
using Naughty = NaughtyAttributes;

namespace FelineFellas
{
    [CreateAssetMenu(menuName = "375/FelineFellas/Card", order = 100)]
    public class CardConfig : ScriptableObject
    {
        [field: SerializeField] public CardIDRef ID     { get; private set; }
        [field: SerializeField] public Rarity    Rarity { get; private set; }

        [field: SerializeField] public CardType Card { get; private set; }

        [field: SerializeField] public int Price { get; private set; }

        [field: SerializeField] public bool CanUseOnlyOnOurRow { get; private set; } = true;

        [field: Naughty.BoxGroup("View")]
        [field: SerializeField] public string Title { get; private set; }

        [field: Naughty.BoxGroup("View")]
        [field: SerializeField] public Sprite Icon { get; private set; }

        [field: Naughty.BoxGroup("Action")]
        [field: Naughty.ShowIf(nameof(Card), CardType.Order)]
        [field: Naughty.HorizontalLine(color: Naughty.EColor.Yellow)]
        [field: SerializeField] public OrderCardConfig OrderCardConfig { get; private set; }

        [field: Naughty.BoxGroup("Unit")]
        [field: Naughty.ShowIf(nameof(Card), CardType.Unit)]
        [field: Naughty.HorizontalLine(color: Naughty.EColor.Blue)]
        [field: SerializeField] public UnitCardConfig UnitCardConfig { get; private set; }

        [field: Naughty.BoxGroup("Enemy AI")]
        [field: Naughty.HorizontalLine(color: Naughty.EColor.Red)]
        [field: SerializeField] public CardEnemyAiConfig EnemyAi { get; private set; }

        // Manually Color Coded For Editor
        [Serializable]
        [JetBrains.Annotations.UsedImplicitly]
        public enum CardType
        {
            Unknown = 0,
            Event = 1, // Green
            Unit = 2,  // Blue
            Order = 3, // Yellow
        }
    }
}
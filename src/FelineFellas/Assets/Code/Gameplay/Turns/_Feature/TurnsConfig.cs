using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public class TurnsConfig
    {
        [field: SerializeField] public TimingsConfig Timings { get; private set; }

        [Serializable]
        public class TimingsConfig
        {
            [field: SerializeField] public float DeckShuffleDuration { get; private set; } = 0.3f;

            [field: SerializeField] public float TmpStartTurnDuration { get; private set; } = 0.2f;
            [field: SerializeField] public float TmpEndTurnDuration   { get; private set; } = 0.2f;

            [field: NaughtyAttributes.BoxGroup("Enemy")]
            [field: SerializeField] public float DelayBetweenEnemyPlayCard { get; private set; } = 0.5f;
        }
    }
}
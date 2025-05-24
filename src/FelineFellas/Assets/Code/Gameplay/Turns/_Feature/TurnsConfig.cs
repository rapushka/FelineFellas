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
            [field: SerializeField] public float PlayerTurnStartedDuration { get; private set; } = 0.2f;

            [field: SerializeField] public float DeckShuffleDuration { get; private set; } = 0.3f;

            [field: SerializeField] public float PlayerTurnEndedDuration  { get; private set; } = 0.2f;
            [field: SerializeField] public float EnemyTurnStartedDuration { get; private set; } = 0.2f;
            [field: SerializeField] public float EnemyTurnDuration        { get; private set; } = 0.5f;
            [field: SerializeField] public float EnemyTurnEndedDuration   { get; private set; } = 0.2f;
        }
    }
}
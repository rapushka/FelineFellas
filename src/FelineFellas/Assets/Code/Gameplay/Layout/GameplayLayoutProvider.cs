using System;
using UnityEngine;

namespace FelineFellas
{
    public interface IGameplayLayoutProvider
    {
        Vector2 PlayerDeck    { get; }
        Vector2 PlayerHand    { get; }
        Vector2 PlayerDiscard { get; }

        Vector2 EnemyDeck    { get; }
        Vector2 EnemyHand    { get; }
        Vector2 EnemyDiscard { get; }
    }

    public class GameplayLayoutProvider : MonoBehaviour, IGameplayLayoutProvider
    {
        [SerializeField] private ActorPoints _player;
        [SerializeField] private ActorPoints _enemy;

        public Vector2 PlayerDeck => _player.Deck.position;

        public Vector2 PlayerHand => _player.Hand.position;

        public Vector2 PlayerDiscard => _player.Discard.position;

        public Vector2 EnemyDeck => _enemy.Deck.position;

        public Vector2 EnemyHand => _enemy.Hand.position;

        public Vector2 EnemyDiscard => _enemy.Discard.position;

        [Serializable]
        private class ActorPoints
        {
            [field: SerializeField] public Transform Deck    { get; private set; }
            [field: SerializeField] public Transform Discard { get; private set; }
            [field: SerializeField] public Transform Hand    { get; private set; }
        }
    }
}
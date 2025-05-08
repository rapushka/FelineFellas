using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public static class CardUtils
    {
        public static Entity<GameScope> AddToDeck(Entity<GameScope> card, Entity<GameScope> deck)
            => card
                .Set<CardInDeck, EntityID>(deck.ID())
                .Set<TargetRotation, float>(0f)
                .Set<TargetScale, float>(1f)
                .Is<Draggable>(true)    // TODO: interactable only after target position is reached?
                .Is<Interactable>(true) // TODO: interactable only after target position is reached?
                .Set<TargetPosition, Vector2>(deck.WorldPosition());

        public static Entity<GameScope> MarkUsedAndDiscard(Entity<GameScope> card)
            => MarkUsed(card)
                .Is<SendToDiscard>(true);

        public static Entity<GameScope> MarkUsed(Entity<GameScope> card)
            => RemoveFromHand(card)
                .Is<Used>(true);

        public static Entity<GameScope> Discard(Entity<GameScope> card)
            => RemoveFromHand(card)
                .Is<SendToDiscard>(true);

        public static Entity<GameScope> RemoveFromHand(Entity<GameScope> card)
            => card
                .Is<WillBeUsed>(false)
                .Remove<InHandIndex>()
                .Is<Interactable>(false);
    }
}
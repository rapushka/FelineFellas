using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public static class CardUtils
    {
        private static PrimaryEntityIndex<GameScope, CellCoordinates, Coordinates> CellIndex
            => Contexts.Instance.Get<GameScope>().GetPrimaryIndex<CellCoordinates, Coordinates>();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static MoneyConfig.ShopViewConfig ShopViewConfig => GameConfig.Money.ShopView;

        private static IRandomService RandomService => ServiceLocator.Resolve<IRandomService>();

        public static Entity<GameScope> AddToDeck(Entity<GameScope> card, Entity<GameScope> deck)
            => card
                .Set<CardInDeck, EntityID>(deck.ID())
                .Set<TargetRotation, float>(0f)
                .Set<TargetScale, float>(1f)
                .Is<Draggable>(true)    // TODO: interactable only after target position is reached?
                .Is<Interactable>(true) // TODO: interactable only after target position is reached?
                .Set<TargetPosition, Vector2>(deck.WorldPosition())
                .Set<CardFace, Face>(Face.FaceDown)

                // cleanups
                .Is<SendToDiscard>(false)
                .Is<InDiscard>(false)
                .Is<Used>(false)
                .RemoveSafely<InHandIndex>();

        public static void DrawCardToHand(Entity<GameScope> card, int index)
        {
            card
                .Remove<CardInDeck>()
                .Add<InHandIndex, int>(index)
                .Set<CardFace, Face>(Face.FaceUp)
                ;
        }

        public static Entity<GameScope> MarkUsedAndDiscard(Entity<GameScope> card)
            => MarkUsed(card)
                .Is<SendToDiscard>(true)
                .Set<CardFace, Face>(Face.FaceDown);

        public static Entity<GameScope> MarkUsed(Entity<GameScope> card)
            => RemoveFromHand(card)
                .Is<Used>(true);

        public static Entity<GameScope> Discard(Entity<GameScope> card)
            => RemoveFromHand(card)
                .Chain(RemoveFromShop)
                .Is<SendToDiscard>(true)
                .Set<TargetRotation, float>(RandomService.Range(-2f, 2f))
                .Set<CardFace, Face>(Face.FaceDown);

        public static Entity<GameScope> RemoveFromHand(Entity<GameScope> card)
            => card
                .Is<WillBeUsed>(false)
                .RemoveSafely<InHandIndex>()
                .Is<Interactable>(false);

        public static Entity<GameScope> PlaceCardInShop(Entity<GameScope> card, Entity<GameScope> slot)
        {
            card
                .Chain(RemoveCardFromPlacedCell)
                .Set<Rotation, float>(ShopViewConfig.SlotRotation)
                .Set<TargetPosition, Vector2>(slot.WorldPosition())
                .Add<CardInShopSlot, EntityID>(slot.ID())
                .Set<CardFace, Face>(Face.FaceUp)
                ;

            slot
                .Is<Empty>(false)
                .Set<PlacedCard, EntityID>(card.ID())
                ;

            return card;
        }

        public static Entity<GameScope> PlaceCardOnGrid(Entity<GameScope> card, Coordinates coordinates)
        {
            var cell = CellIndex.GetEntity(coordinates);

            card
                .Chain(RemoveCardFromPlacedCell)
                .Set<TargetRotation, float>(0f)
                .Set<TargetPosition, Vector2>(cell.WorldPosition())
                .Set<OnField, Coordinates>(cell.Get<CellCoordinates>().Value)
                .Set<CardFace, Face>(Face.FaceUp)
                ;

            cell
                .Is<Empty>(false)
                .Set<PlacedCard, EntityID>(card.ID())
                ;

            return card;
        }

        private static Entity<GameScope> RemoveCardFromPlacedCell(Entity<GameScope> card)
        {
            if (!card.TryGet<OnField, Coordinates>(out var coordinates))
                return card;

            var cell = CellIndex.GetEntity(coordinates);
            cell
                .Remove<PlacedCard>()
                .Is<Empty>(true)
                ;

            return card;
        }

        private static Entity<GameScope> RemoveFromShop(Entity<GameScope> card)
        {
            if (!card.TryGet<CardInShopSlot, EntityID>(out var slotID))
                return card;

            slotID.GetEntity()
                .Remove<PlacedCard>()
                .Is<Empty>(true)
                ;

            return card;
        }
    }
}
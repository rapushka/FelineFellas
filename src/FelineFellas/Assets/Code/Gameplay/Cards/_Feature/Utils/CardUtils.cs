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
                .Set<TargetRotation, float>(deck.Get<Rotation, float>())
                .Set<TargetScale, float>(1f)
                .Is<Draggable>(false)
                .Is<Interactable>(false)
                .Set<TargetPosition, Vector2>(deck.WorldPosition())
                .Set<CardFace, Face>(Face.FaceDown)
                .Set<ChildOf, EntityID>(deck.ID())

                // cleanups
                .Is<SendToDiscard>(false)
                .Is<InDiscard>(false)
                .Is<Used>(false)
                .RemoveSafely<InHandIndex>();

        public static Entity<GameScope> DrawCardToHand(Entity<GameScope> card, int index)
            => card
                .Remove<CardInDeck>()
                .Add<InHandIndex, int>(index)
                .Set<CardFace, Face>(Face.FaceUp)
                .Is<Draggable>(true)
                .Is<Interactable>(true);

        public static Entity<GameScope> MarkUsedAndDiscard(Entity<GameScope> card)
            => MarkUsed(card)
                .Is<SendToDiscard>(true)
                .Set<CardFace, Face>(Face.FaceDown);

        public static Entity<GameScope> MarkUsed(Entity<GameScope> card)
            => RemoveFromHand(card)
                .Is<Used>(true);

        public static Entity<GameScope> Purchase(Entity<GameScope> card, EntityID playerID)
        {
            card.Pop<CardInShopSlot, EntityID>().GetEntity()
                .Remove<PlacedCard>()
                .Is<Empty>(true)
                ;

            return card
                    .AssignToSide(Side.Player)
                    .Chain(Discard)
                    .Set<ChildOf, EntityID>(playerID)
                ;
        }

        public static Entity<GameScope> Discard(Entity<GameScope> card)
        {
            var randomRotation = card.Get<OnSide>().Value.Visit(
                onPlayer: () => RandomService.Range(-2f, 2f),
                onEnemy: () => RandomService.Range(178f, 182f)
            );

            return card
                    .Chain(RemoveFromHand)
                    .Chain(RemoveCardFromPlacedCell)
                    .Is<SendToDiscard>(true)
                    .Set<TargetRotation, float>(randomRotation)
                    .Set<CardFace, Face>(Face.FaceDown)
                ;
        }

        public static Entity<GameScope> RemoveFromHand(Entity<GameScope> card)
            => card
                .Is<WillBeUsed>(false)
                .RemoveSafely<InHandIndex>()
                .Is<Interactable>(false)
                .Is<Draggable>(false);

        public static Entity<GameScope> PlaceCardInShop(Entity<GameScope> card, Entity<GameScope> slot)
        {
            var slotID = slot.ID();
            card
                .Chain(RemoveCardFromPlacedCell)
                .Set<Rotation, float>(ShopViewConfig.SlotRotation)
                .Set<TargetPosition, Vector2>(slot.WorldPosition())
                .Add<CardInShopSlot, EntityID>(slotID)
                .Set<CardFace, Face>(Face.FaceUp)
                .Set<ChildOf, EntityID>(slotID)
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

#if DEBUG
            // ReSharper disable once RedundantNameQualifier – to make this peace of code independant from surrounding
            if (!card.Is<UnitCard>())
                UnityEngine.Debug.LogError("Only Units can be placed on field!");
#endif

            card
                .Chain(RemoveCardFromPlacedCell)
                .Set<TargetRotation, float>(0f)
                .Set<TargetPosition, Vector2>(cell.WorldPosition())
                .Set<OnField, Coordinates>(cell.Get<CellCoordinates>().Value)
                .Set<CardFace, Face>(Face.FaceUp)
                .Is<Interactable>(false)
                .Is<Draggable>(false)
                .Set<ChildOf, EntityID>(cell.ID())
                ;

            cell
                .Is<Empty>(false)
                .Set<PlacedCard, EntityID>(card.ID())
                ;

            return card;
        }

        public static Entity<GameScope> CleanupUsedCard(Entity<GameScope> card)
            => card.RemoveSafely<SelectedTarget>()
                .RemoveSafely<UseTarget>()
                .Is<WillBeUsed>(false);

        private static Entity<GameScope> RemoveCardFromPlacedCell(Entity<GameScope> card)
        {
            var cardIsOnField = card.TryGet<OnField, Coordinates>(out var coordinates);
            if (!cardIsOnField)
                return card;

            var cell = CellIndex.GetEntity(coordinates);
            cell
                .Remove<PlacedCard>()
                .Is<Empty>(true)
                ;

            card.Remove<OnField>();

            return card;
        }
    }
}
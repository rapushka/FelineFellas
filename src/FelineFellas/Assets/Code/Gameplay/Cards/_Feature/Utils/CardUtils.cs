using UnityEngine;
using GameEntity = Entitas.Generic.Entity<FelineFellas.GameScope>;

namespace FelineFellas
{
    public static class CardUtils
    {
        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static MoneyConfig.ShopViewConfig ShopViewConfig => GameConfig.Money.ShopView;

        private static IRandomService RandomService => ServiceLocator.Resolve<IRandomService>();

        public static GameEntity AddToDeck(GameEntity card, GameEntity deck)
            => card
                .Set<CardInDeck, EntityID>(deck.ID())
                .Set<TargetRotation, float>(deck.Get<Rotation, float>())
                .Set<TargetScale, float>(1f)
                .Is<Draggable>(false)
                .Is<Interactable>(false)
                .Set<TargetPosition, Vector2>(deck.WorldPosition())
                .Set<CardFace, Face>(Face.FaceDown)
                .Set<ChildOf, EntityID>(deck.ID())
                .SetSorting(RenderOrder.CardInDeck)

                // cleanups
                .Is<SendToDiscard>(false)
                .Is<InDiscard>(false)
                .Is<Used>(false)
                .RemoveSafely<InHandIndex>();

        public static GameEntity DrawCardToHand(GameEntity card, int index)
            => card
                .Remove<CardInDeck>()
                .Add<InHandIndex, int>(index)
                .Set<CardFace, Face>(Face.FaceUp)
                .Is<Draggable>(true)
                .Is<Interactable>(true);

        public static GameEntity MarkUsedAndDiscard(GameEntity card)
            => MarkUsed(card)
                .Is<SendToDiscard>(true)
                .Set<CardFace, Face>(Face.FaceDown);

        public static GameEntity MarkUsed(GameEntity card)
            => RemoveFromHand(card)
                .Is<Used>(true);

        public static GameEntity Purchase(GameEntity card, EntityID playerID)
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

        public static GameEntity Discard(GameEntity card)
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
                    .SetSorting(RenderOrder.CardInDiscard)
                ;
        }

        public static GameEntity RemoveFromHand(GameEntity card)
            => card
                .Is<WillBeUsed>(false)
                .RemoveSafely<InHandIndex>()
                .Is<Interactable>(false)
                .Is<Draggable>(false);

        public static GameEntity PlaceCardInShop(GameEntity card, GameEntity slot)
        {
            var slotID = slot.ID();
            card
                .Chain(RemoveCardFromPlacedCell)
                .Set<Rotation, float>(ShopViewConfig.SlotRotation)
                .Set<TargetPosition, Vector2>(slot.WorldPosition())
                .Add<CardInShopSlot, EntityID>(slotID)
                .Set<CardFace, Face>(Face.FaceUp)
                .Set<ChildOf, EntityID>(slotID)
                .SetSorting(RenderOrder.CardInShop)
                ;

            slot
                .Is<Empty>(false)
                .Set<PlacedCard, EntityID>(card.ID())
                ;

            return card;
        }

        public static GameEntity PlaceCardOnField(GameEntity card, GameEntity cell)
        {
#if DEBUG
            // ReSharper disable once RedundantNameQualifier – to make this peace of code independant from surrounding
            if (!card.Is<UnitCard>())
                UnityEngine.Debug.LogError("Only Units can be placed on field!");
#endif

            var cellID = cell.ID();

            card
                .Chain(RemoveCardFromPlacedCell)
                .Set<TargetRotation, float>(0f)
                .Set<TargetPosition, Vector2>(cell.WorldPosition())
                .Set<CardFace, Face>(Face.FaceUp)
                .Is<Interactable>(false)
                .Is<Draggable>(false)
                .Is<OnField>(true)
                .Set<ChildOf, EntityID>(cellID)
                .SetSorting(RenderOrder.CardOnField)
                ;

            cell
                .Is<Empty>(false)
                .Set<PlacedCard, EntityID>(card.ID())
                ;

            return card;
        }

        public static GameEntity CleanupUsedCard(GameEntity card)
            => card.RemoveSafely<TargetSubject>()
                .RemoveSafely<DropCardOn>()
                .Is<WillBeUsed>(false)
                .Is<CanNotPlay>(false);

        private static GameEntity RemoveCardFromPlacedCell(GameEntity card)
        {
            if (!card.Is<OnField>())
                return card;

            var cell = card.Get<ChildOf>().Value.GetEntity();
            cell
                .Remove<PlacedCard>()
                .Is<Empty>(true)
                ;

            return card
                    .Remove<OnField>()
                ;
        }
    }
}
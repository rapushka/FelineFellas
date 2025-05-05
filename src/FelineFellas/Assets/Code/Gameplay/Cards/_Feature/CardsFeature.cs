namespace FelineFellas
{
    public sealed class CardsFeature : Feature
    {
        public CardsFeature()
            : base(nameof(CardsFeature))
        {
            Add(new SpawnDeckSystem());
            Add(new DrawCardsOnStartSystem());

            Add(new UseGlobalCardSystem());
            Add(new UseUnitCardSystem());
            Add(new UseDroppedCardsIfCanSystem());

            Add(new MoveDiscardedCardsToDiscardPileSystem());
            Add(new RecalculateCardsIndexesInHandSystem());

            // View
            Add(new ArrangeCardsInHandSystem());
            Add(new CalculateCardViewScaleSystem());

            Add(new CleanupUsedSystem());
        }
    }
}
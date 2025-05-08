namespace FelineFellas
{
    public sealed class CardsFeature : Feature
    {
        public CardsFeature()
            : base(nameof(CardsFeature))
        {
            Add(new SpawnDeckWithCardsSystem());
            Add(new StartWithPlayerTurnSystem());

            Add(new TickEnemyTurnSystem());

            // # on turn started
            Add(new SendDrawCardsIfDeckDoesNotNeedShuffleSystem());

            // shuffle cards
            Add(new StartDeckShufflingTimerSystem());
            Add(new ShuffleDeckSystem());
            Add(new TickDeckShufflingTimerSystem());
            Add(new DrawCardsIfDeckShuffleEndedSystem());

            Add(new DrawCardsFromDeckSystem());
            Add(new UpdateDeckNeedsShuffleAfterDrawSystem());

            // # on turn ended
            Add(new DiscardAllCardsOnTurnEndedSystem());

            Add(new OnPlayerTurnEndedStartEnemyTurnSystem());

            Add(new ResetCardWillBeUsed());
            Add(new UseGlobalCardSystem());
            Add(new UseUnitCardSystem());

            Add(new UseDroppedOneShotCardsIfCanSystem());
            Add(new PlaceUnitCardsSystems());

            Add(new MoveDiscardedCardsToDiscardPileSystem());
            Add(new RecalculateCardsIndexesInHandSystem());

            // View
            Add(new ArrangeCardsInHandSystem());
            Add(new CalculateCardViewScaleSystem());

            Add(new CleanupUsedSystem());
        }
    }
}
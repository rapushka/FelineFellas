namespace FelineFellas
{
    public sealed class CardsFeature : Feature
    {
        private static IGameMode GameMode => ServiceLocator.Resolve<IGameModeService>().CurrentGameMode;

        public CardsFeature()
            : base(nameof(CardsFeature))
        {
            Add(new SpawnDeckWithCardsSystem());
            Add(new SpawnPlayerLeaderSystem());
            Add(new SpawnEnemyUnitsSystem());
            Add(new StartWithPlayerTurnSystem());

            Add(new TickEnemyTurnSystem());

            // # on turn started
            Add(new CardsDrawFeature());

            // # on turn ended
            if (GameMode.DiscardHandOnEndTurn)
            {
                Add(new DiscardAllCardsOnTurnEndedSystem());
            }

            Add(new OnPlayerTurnEndedStartEnemyTurnSystem());

            Add(new ResetCardWillBeUsed());
            Add(new CheckGlobalCardUseSystem());
            Add(new CheckUnitCardUseSystem());
            Add(new CheckActionCardUseSystem());

            Add(new UseDroppedOneShotCardsIfCanSystem());
            Add(new UseCardAbilitiesFeature());
            Add(new PlaceUnitCardsSystems());

            Add(new MoveDiscardedCardsToDiscardPileSystem());
            Add(new RecalculateCardsIndexesInHandSystem());

            Add(new DestroyDeadEntitiesSystem()); // This order to make dead entity exist 1 frame longer to handle its death if needed
            Add(new MarkUnitsWithZeroHpDeadSystem());

            // View
            Add(new ArrangeCardsInHandSystem());
            Add(new CalculateCardViewScaleSystem());

            Add(new CleanupUsedSystem());
        }
    }
}
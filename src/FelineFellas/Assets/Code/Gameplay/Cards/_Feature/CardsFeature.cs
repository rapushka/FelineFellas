namespace FelineFellas
{
    public sealed class CardsFeature : Feature
    {
        private static IGameMode GameMode => ServiceLocator.Resolve<IGameModeService>().CurrentGameMode;

        public CardsFeature()
            : base(nameof(CardsFeature))
        {
            // # on turn started
            Add(new CardsDrawFeature());

            // # on turn ended
            if (GameMode.DiscardHandOnEndTurn)
            {
                Add(new DiscardAllPlayerCardsOnTurnEndedSystem());
                Add(new DiscardAllEnemyCardsOnEnemyTurnEndedSystem());
            }

            Add(new ResetCardWillBeUsed());
            Add(new CheckGlobalCardUseSystem());
            Add(new CheckUnitCardUseSystem());
            Add(new CheckActionCardUseSystem());

            Add(new UseDroppedOneShotCardsIfCanSystem());
            Add(new UseCardAbilitiesFeature());
            Add(new PlaceUnitCardsSystems());

            Add(new MoveDiscardedCardsToDiscardPileSystem());
            Add(new SendRecalculateIndexesRequestOnCardUsedSystem());
            Add(new RecalculateCardsIndexesInHandOnRequestSystem());
            Add(new CleanupCardsInDiscardSystem());

            Add(new ResetUsageLimitOnTurnEnded<OnPlayerTurnEndedState, PlayerCard>());
            Add(new ResetUsageLimitOnTurnEnded<OnEnemyTurnEndedState, EnemyCard>());

            Add(new DestroyDeadEntitiesSystem()); // This order to make dead entity exist 1 frame longer to handle its death if needed
            Add(new MarkUnitsWithZeroHpDeadSystem());
            Add(new GameOverIfLeaderDiedSystem());

            // View
            Add(new ArrangeCardsInHandSystem());
            Add(new CalculateCardViewScaleSystem());

            Add(new CleanupUsedSystem());
        }
    }
}
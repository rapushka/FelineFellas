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

            Add(new CardWillBeUsedFeature());

            Add(new UseAndDiscardDroppedCardsSystem());
            Add(new UseCardAbilitiesFeature());
            Add(new PlaceUnitCardsSystems());

            Add(new MoveDiscardedCardsToDiscardPileSystem());
            Add(new SendRecalculateIndexesRequestOnCardUsedSystem());
            Add(new RecalculateCardsIndexesInHandOnRequestSystem());
            Add(new CleanupCardsInDiscardSystem());

            Add(new ResetStaminaOnTurnEnded<OnPlayerTurnEndedState, PlayerCard>());
            Add(new ResetStaminaOnTurnEnded<OnEnemyTurnEndedState, EnemyCard>());

            // At first destroy dead units, and only then mark new units dead
            // to make dead entity exist 1 frame longer to handle its death if needed
            Add(new GameOverIfPlayerLeaderDiedSystem());
            Add(new DefeatEnemyLeaderOnDeathSystem());
            Add(new DestroyDeadEntitiesSystem());
            Add(new MarkUnitsWithZeroHpDeadSystem());

            // View
            Add(new ArrangeCardsInHandSystem());
            Add(new CalculateCardViewScaleSystem());

            Add(new CleanupUsedSystem());
        }
    }
}
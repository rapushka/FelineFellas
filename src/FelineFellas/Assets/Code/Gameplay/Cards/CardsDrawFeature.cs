namespace FelineFellas
{
    public sealed class CardsDrawFeature : Feature
    {
        public CardsDrawFeature()
            : base(nameof(CardsDrawFeature))
        {
            Add(new CheckIfActorShouldDrawCardsSystem());

            Add(new UpdateActorWaitingForDeckShuffleSystem());
            Add(new StopDrawingCardsIfNoCardsLeftSystem());
            Add(new UpdateDeckNeedsShuffleSystem());

            // shuffle cards
            Add(new StartDeckShufflingTimerSystem());
            Add(new ShuffleDeckSystem());
            Add(new TickDeckShufflingTimerSystem());
            Add(new SendDrawCardsOnDeckShuffleEndedSystem());

            Add(new DrawCardsFromDeckSystem());

            Add(new TransitToNextTurnStateIfNoActorsDrawCardsSystem());
            Add(new OnDrawCardsCompletedResetActorComponentsSystem());
        }
    }
}
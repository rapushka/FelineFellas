namespace FelineFellas
{
    public sealed class CardsDrawFeature : Feature
    {
        public CardsDrawFeature()
            : base(nameof(CardsDrawFeature))
        {
            Add(new StartDrawingCardsIfNeedsOnTurnStartedSystem());
            Add(new StopDrawingCardsIfNoCardsInDiscardSystem());
            Add(new UpdateDeckNeedsShuffleSystem());
            Add(new SendDrawCardsOnTurnStartSystem());

            // shuffle cards
            Add(new StartDeckShufflingTimerSystem());
            Add(new ShuffleDeckSystem());
            Add(new TickDeckShufflingTimerSystem());
            Add(new SendDrawCardsOnDeckShuffleEndedSystem());

            Add(new DrawCardsFromDeckSystem());
            Add(new FinishDrawingCardsSystem());
        }
    }
}
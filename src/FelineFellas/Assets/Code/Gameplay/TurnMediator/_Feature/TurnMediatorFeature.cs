namespace FelineFellas
{
    public sealed class TurnMediatorFeature : Feature
    {
        public TurnMediatorFeature()
            : base(nameof(TurnMediatorFeature))
        {
            Add(new CreateTurnMediatorSystem());
            Add(new TickChangeStateAfterDelaySystem());
            Add(new ToNextTurnStateSystem());
            Add(new StartPlayerTurnOnFightStartSystem());

            Add(new StartTimerIfInTemporaryStateSystem());
        }
    }
}
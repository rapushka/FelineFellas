namespace FelineFellas
{
    public sealed class EnemyAiFeature : Feature
    {
        public EnemyAiFeature()
            : base(nameof(EnemyAiFeature))
        {
            Add(new StartDelayBeforeCardPlaySystem());
            Add(new TickDelayBeforeCardPlaySystem());

            Add(new FindCardWithMostPrioritySystem());
            Add(new SelectUnitToApplyActionCard());

            Add(new EndEnemyTurnIfHasNoCardsInHandSystem());

            Add(new CleanupEnemySystem());
        }
    }
}
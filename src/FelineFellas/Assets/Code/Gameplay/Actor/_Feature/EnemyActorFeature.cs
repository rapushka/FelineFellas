namespace FelineFellas
{
    public sealed class EnemyActorFeature : Feature
    {
        public EnemyActorFeature()
            : base(nameof(EnemyActorFeature))
        {
            Add(new CreateEnemyActorsSystem());

            Add(new ArrangeStagesOnEnemyInitializationSystem());
            Add(new UpdateMoneyViewSystem());
            Add(new EnemyAiFeature());
        }
    }
}
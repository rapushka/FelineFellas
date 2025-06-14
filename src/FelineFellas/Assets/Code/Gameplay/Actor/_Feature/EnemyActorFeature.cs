namespace FelineFellas
{
    public sealed class EnemyActorFeature : Feature
    {
        public EnemyActorFeature()
            : base(nameof(EnemyActorFeature))
        {
            Add(new CreateEnemyActorSystem());

            Add(new UpdateMoneyViewSystem());
            Add(new EnemyAiFeature());
        }
    }
}
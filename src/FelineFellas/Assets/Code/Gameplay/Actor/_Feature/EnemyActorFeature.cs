namespace FelineFellas
{
    public sealed class EnemyActorFeature : Feature
    {
        public EnemyActorFeature()
            : base(nameof(EnemyActorFeature))
        {
            Add(new CreateEnemyActorsSystem());

            Add(new StartFightOnClickSystem());
            Add(new OnStartFightHideAllEnemiesSystem());

            Add(new ArrangeStagesOnEnemyInitializationSystem());
            Add(new UpdateMoneyViewSystem()); // TODO: what this is doing here?
            Add(new EnemyAiFeature());
        }
    }
}
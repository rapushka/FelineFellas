namespace FelineFellas
{
    public sealed class EnemyActorFeature : Feature
    {
        public EnemyActorFeature()
            : base(nameof(EnemyActorFeature))
        {
            Add(new CreateEnemyActorsSystem());
            Add(new CreateDeckForCurrentEnemySystem());
            Add(new ActivateNextEnemy());

            Add(new StartFightOnClickSystem());
            Add(new OnStartFightHideAllEnemiesSystem());
            Add(new OnStartFightHideMapUiSystem());

            Add(new RequestArrangeStagesOnEnemyInitializationSystem());
            Add(new RequestArrangeStagesOnStageCompletedSystem());
            Add(new ArrangeStagesSystem());

            Add(new EnemyAiFeature());
        }
    }
}
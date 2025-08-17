namespace FelineFellas
{
    public sealed class StageFeature : Feature
    {
        public StageFeature()
            : base(nameof(StageFeature))
        {
            Add(new CreateStagesOnMapCreatedSystem());
            Add(new CompleteStageLoadingSystem());

            Add(new ReturnAllPlayerCardToDeckOnStageCompletedSystem());
            Add(new ReparentEnemyLeadOnStageCompletedSystem());
            Add(new DestroyFieldOnStageCompletedSystem());
            Add(new DestroyDefeatedActorOnStageCompletedSystem());
            Add(new ShowMapOnStageOnStageCompletedSystem());
            Add(new OnStageCompletedEventProcessedSystem());
        }
    }
}
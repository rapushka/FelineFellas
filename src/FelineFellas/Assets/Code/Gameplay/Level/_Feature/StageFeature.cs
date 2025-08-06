namespace FelineFellas
{
    public sealed class StageFeature : Feature
    {
        public StageFeature()
            : base(nameof(StageFeature))
        {
            Add(new CreateStagesOnMapCreatedSystem());
            Add(new CompleteStageLoadingSystem());
        }
    }
}
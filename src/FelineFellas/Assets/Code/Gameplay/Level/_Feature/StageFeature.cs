namespace FelineFellas
{
    public sealed class StageFeature : Feature
    {
        public StageFeature()
            : base(nameof(StageFeature))
        {
            Add(new CreateStageSystem());
            Add(new CompleteStageLoadingSystem());
        }
    }
}
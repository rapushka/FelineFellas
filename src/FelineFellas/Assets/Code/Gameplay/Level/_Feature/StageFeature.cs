namespace FelineFellas
{
    public sealed class StageFeature : Feature
    {
        public StageFeature()
            : base(nameof(StageFeature))
        {
            Add(new CreateStageOnFightStartSystem());
            Add(new CompleteStageLoadingSystem());
        }
    }
}
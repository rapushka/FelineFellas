namespace FelineFellas
{
    public sealed class LevelFeature : Feature
    {
        public LevelFeature()
            : base(nameof(LevelFeature))
        {
            Add(new CreateLevelSystem());
        }
    }
}
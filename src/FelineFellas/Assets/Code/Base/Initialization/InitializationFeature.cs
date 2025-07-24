namespace FelineFellas
{
    public sealed class InitializationFeature : Feature
    {
        public InitializationFeature()
            : base(nameof(InitializationFeature))
        {
            Add(new EndEntityInitializationSystem());
        }
    }
}
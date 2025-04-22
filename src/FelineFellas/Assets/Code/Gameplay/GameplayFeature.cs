namespace FelineFellas
{
    public sealed class GameplayFeature : Feature
    {
        public GameplayFeature()
            : base(nameof(GameplayFeature))
        {
            Add(new GridFeature());

            Add(new BoilerplateFeature());
        }
    }
}
namespace FelineFellas
{
    public sealed class GridFeature : Feature
    {
        public GridFeature()
            : base(nameof(GridFeature))
        {
            Add(new SpawnFieldSystem());
            Add(new SpawnGridSystem());
        }
    }
}
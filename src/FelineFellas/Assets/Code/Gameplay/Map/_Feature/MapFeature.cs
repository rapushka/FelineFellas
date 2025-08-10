namespace FelineFellas
{
    public sealed class MapFeature : Feature
    {
        public MapFeature()
            : base(nameof(MapFeature))
        {
            Add(new CreateMapSystem());

            Add(new ShowMapViewOnMapInitializedSystem());
        }
    }
}
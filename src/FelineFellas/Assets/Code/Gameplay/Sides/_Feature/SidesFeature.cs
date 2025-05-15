namespace FelineFellas
{
    public sealed class SidesFeature : Feature
    {
        public SidesFeature()
            : base(nameof(SidesFeature))
        {
            Add(new CreatePlayerSideSystem());

            Add(new UpdateMoneyViewSystem());
        }
    }
}
namespace FelineFellas
{
    public sealed class PlayerActorFeature : Feature
    {
        public PlayerActorFeature()
            : base(nameof(PlayerActorFeature))
        {
            Add(new CreatePlayerActorSystem());

            Add(new UpdateMoneyViewSystem());
        }
    }
}
namespace FelineFellas
{
    public sealed class ActorFeature : Feature
    {
        public ActorFeature()
            : base(nameof(ActorFeature))
        {
            Add(new CreatePlayerActorSystem());

            Add(new UpdateMoneyViewSystem());
        }
    }
}
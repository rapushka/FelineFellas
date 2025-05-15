namespace FelineFellas
{
    public sealed class GameplayFeature : Feature
    {
        public GameplayFeature()
            : base(nameof(GameplayFeature))
        {
            Add(new InputFeature());

            Add(new SidesFeature());
            Add(new GridFeature());
            Add(new CardsFeature());

            Add(new MoveToPositionSystem());
            Add(new RotateToTargetSystem());
            Add(new AnimateScaleSystem());

            Add(new ResetSortingOrderSystem());
            Add(new UpdateSortingOrderForCardsInHandSystem());

            Add(new BoilerplateFeature());
            Add(new DestroyEntitiesSystem());
        }
    }
}
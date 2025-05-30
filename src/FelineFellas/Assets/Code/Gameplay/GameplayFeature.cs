namespace FelineFellas
{
    public sealed class GameplayFeature : Feature
    {
        public GameplayFeature()
            : base(nameof(GameplayFeature))
        {
            Add(new InputFeature());

            Add(new TurnMediatorFeature());
            Add(new LevelFeature());
            Add(new GridFeature());

            Add(new ActorFeature());

            Add(new CardsFeature());
            Add(new ShopFeature());

            Add(new MoveToPositionSystem());
            Add(new RotateToTargetSystem());
            Add(new AnimateScaleSystem());

            Add(new ResetSortingOrderSystem());
            Add(new UpdateSortingOrderForCardsInHandSystem());
            Add(new UpdateSortingOrderForDraggingCardSystem());

            Add(new BoilerplateFeature());
            Add(new DestroyWithChildrenSystem());
            Add(new DestroyEntitiesSystem());
        }
    }
}
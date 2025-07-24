namespace FelineFellas
{
    public sealed class GameplayFeature : Feature
    {
        public GameplayFeature()
            : base(nameof(GameplayFeature))
        {
            Add(new InputFeature());

            Add(new InitializationFeature());

            Add(new MapFeature());
            Add(new TurnMediatorFeature());

            Add(new PlayerActorFeature());
            Add(new StageFeature());

            Add(new EnemyActorFeature());
            Add(new FieldFeature());

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
using UnityEngine;

namespace FelineFellas
{
    public sealed class GameplayFeature : Feature
    {
        private static IGameModeService GameModeService => ServiceLocator.Resolve<IGameModeService>();

        public GameplayFeature()
            : base(nameof(GameplayFeature))
        {
            Debug.Log(GameModeService.CurrentGameMode);

            Add(new InputFeature());

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
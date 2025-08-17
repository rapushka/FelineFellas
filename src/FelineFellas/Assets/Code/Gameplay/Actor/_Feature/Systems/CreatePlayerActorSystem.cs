using Entitas;

namespace FelineFellas
{
    public class CreatePlayerActorSystem : IInitializeSystem
    {
        private static IGameConfig   GameConfig   => ServiceLocator.Resolve<IGameConfig>();
        private static IActorFactory ActorFactory => ServiceLocator.Resolve<IActorFactory>();

        private static IStageFactory StageFactory => ServiceLocator.Resolve<IStageFactory>();

        public void Initialize()
        {
            var mockStage = StageFactory.CreateMockForPlayer();
            var stageID = mockStage.Get<Stage>().Value;
            ActorFactory.CreatePlayer(GameConfig.Loadouts.PlayerLoadout, stageID)
                .SetParent(mockStage);
        }
    }
}
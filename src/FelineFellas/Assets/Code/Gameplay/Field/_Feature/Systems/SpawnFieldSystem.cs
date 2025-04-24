using Entitas;

namespace FelineFellas
{
    public sealed class SpawnFieldSystem : IInitializeSystem
    {
        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static IFieldFactory FieldFactory => ServiceLocator.Resolve<IFieldFactory>();

        private static FieldConfig FieldConfig => GameConfig.Field;

        public void Initialize()
        {
            FieldFactory.CreateField(FieldConfig.View.FieldCenter);
        }
    }
}
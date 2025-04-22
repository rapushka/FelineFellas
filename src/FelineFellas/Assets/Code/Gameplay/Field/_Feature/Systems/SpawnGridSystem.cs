using Entitas;

namespace FelineFellas
{
    public sealed class SpawnGridSystem : IInitializeSystem
    {
        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static IViewFactory ViewFactory => ServiceLocator.Resolve<IViewFactory>();

        private static FieldConfig FieldConfig => GameConfig.Field;

        public void Initialize()
        {
            var size = FieldConfig.FieldSize;

            for (var column = 0; column < size.Width; column++)
            for (var row = 0; row < size.Height; row++)
            {
                ViewFactory.CreateInWorld(FieldConfig.ViewPrefab, new(column, row));
            }
        }
    }
}
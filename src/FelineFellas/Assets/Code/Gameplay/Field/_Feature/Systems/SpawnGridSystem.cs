using Entitas;
using UnityEngine;

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
            var spacings = FieldConfig.View.Spacings;
            var halfSizes = new Vector2((size.Width - 1) / 2f, (size.Height - 1) / 2f);

            for (var column = 0; column < size.Width; column++)
            for (var row = 0; row < size.Height; row++)
            {
                var position = (new Vector2(column, row) - halfSizes) * spacings;

                ViewFactory.CreateInWorld(FieldConfig.View.ViewPrefab, position);
            }
        }
    }
}
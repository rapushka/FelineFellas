using Entitas;
using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public sealed class SpawnGridSystem : IInitializeSystem
    {
        private readonly IGroup<Entity<GameScope>> _fields
            = GroupBuilder<GameScope>
                .With<Field>()
                .And<WorldPosition>()
                .Build();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static IFieldFactory FieldFactory => ServiceLocator.Resolve<IFieldFactory>();

        private static FieldConfig FieldConfig => GameConfig.Field;

        public void Initialize()
        {
            foreach (var field in _fields)
            {
                var center = field.Get<WorldPosition>().Value;

                var size = FieldConfig.FieldSize;
                var spacings = FieldConfig.View.Spacings;
                var halfSizes = new Vector2((size.Width - 1) / 2f, (size.Height - 1) / 2f);

                for (var column = 0; column < size.Width; column++)
                for (var row = 0; row < size.Height; row++)
                {
                    var position = (new Vector2(column, row) - halfSizes) * spacings;
                    FieldFactory.CreateCell(center + position, new(row, column));
                }
            }
        }
    }
}
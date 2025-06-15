using Entitas;
using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public sealed class SpawnCellsSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _stages
            = GroupBuilder<GameScope>
                .With<Stage>()
                .And<EnteringStage>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _rows
            = GroupBuilder<GameScope>
                .With<Row>()
                .Build();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static IFieldFactory FieldFactory => ServiceLocator.Resolve<IFieldFactory>();

        private static FieldConfig FieldConfig => GameConfig.Field;

        public void Execute()
        {
            foreach (var _ in _stages)
            foreach (var row in _rows)
            {
                var rowID = row.ID();
                var center = row.WorldPosition();

                var width = FieldConfig.RowWidth;
                var spacings = FieldConfig.View.Spacings;
                var halfSizes = new Vector2((width - 1) / 2f, 0);

                for (var i = 0; i < width; i++)
                {
                    var position = (new Vector2(i, 0) - halfSizes) * spacings;
                    FieldFactory.CreateCell(center + position, i)
                        .Add<ChildOf, EntityID>(rowID)
                        .Add<OnSide, Side>(row.Get<OnSide>().Value);
                }
            }
        }
    }
}
using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public interface IFieldFactory : IService
    {
        Entity<GameScope> CreateField();
        Entity<GameScope> CreateRow(Side side, Entity<GameScope> field);

        Entity<GameScope> CreateCell(Vector2 position, int index);
    }

    public class FieldFactory : IFieldFactory
    {
        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static IViewFactory ViewFactory => ServiceLocator.Resolve<IViewFactory>();

        private static FieldConfig FieldConfig => GameConfig.Field;

        private static FieldConfig.ViewConfig ViewConfig => FieldConfig.View;

        public Entity<GameScope> CreateField()
        {
            var entity = ViewFactory.CreateInWorld(ViewConfig.FieldPrefab, ViewConfig.FieldCenter).Entity;

            return entity
                    .Add<Name, string>("field")
                    .Add<Field>()
                ;
        }

        public Entity<GameScope> CreateRow(Side side, Entity<GameScope> field)
        {
            var position = CalculateRowCenter();
            var entity = ViewFactory.CreateInWorld(ViewConfig.FieldPrefab, position).Entity;

            return entity
                    .Add<Name, string>("row")
                    .Add<Row>()
                    .Add<OnSide, Side>(side)
                    .Is<PlayerRow>(side is Side.Player)
                    .Is<EnemyRow>(side is Side.Enemy)
                ;

            Vector2 CalculateRowCenter()
            {
                var sign = side.Visit(
                    onPlayer: -1,
                    onEnemy: 1
                );
                var yDistance = FieldConfig.View.DistanceBetweenRows;
                var fieldCenter = field.WorldPosition();
                return fieldCenter.Add(y: yDistance * sign);
            }
        }

        public Entity<GameScope> CreateCell(Vector2 position, int index)
        {
            var entity = ViewFactory.CreateInWorld(ViewConfig.CellPrefab, position).Entity;
            return entity
                    .Add<Name, string>("cell")
                    .Add<Cell>()
                    .Add<Interactable>()
                    .Add<Free>()
                    .Add<SpriteSortingGroup, RenderOrder>(RenderOrder.Grid)
                    .Add<CellIndex, int>(index)
                ;
        }
    }
}
using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public interface IFieldFactory : IService
    {
        Entity<GameScope> CreateField(Vector2 position);

        Entity<GameScope> CreateCell(Vector2 position, Coordinates coordinates);
    }

    public class FieldFactory : IFieldFactory
    {
        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static IViewFactory ViewFactory => ServiceLocator.Resolve<IViewFactory>();

        private static FieldConfig FieldConfig => GameConfig.Field;

        public Entity<GameScope> CreateField(Vector2 position)
        {
            var entity = ViewFactory.CreateInWorld(FieldConfig.View.FieldPrefab, position).Entity;
            // TODO: dynamically calculate collider size.. someday..

            return entity
                    .Add<Field>()
                ;
        }

        public Entity<GameScope> CreateCell(Vector2 position, Coordinates coordinates)
        {
            var entity = ViewFactory.CreateInWorld(FieldConfig.View.CellPrefab, position).Entity;
            return entity
                    .Add<Cell>()
                    .Add<Interactable>()
                    .Add<Empty>()
                    .Add<SpriteSortingGroup, SortGroup>(SortGroup.Grid)
                    .Add<CellCoordinates, Coordinates>(coordinates)
                ;
        }
    }
}
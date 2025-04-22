using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public interface IFieldFactory : IService
    {
        Entity<GameScope> CreateCell(Vector2 position);
    }

    public class FieldFactory : IFieldFactory
    {
        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static IViewFactory ViewFactory => ServiceLocator.Resolve<IViewFactory>();

        private static FieldConfig FieldConfig => GameConfig.Field;

        public Entity<GameScope> CreateCell(Vector2 position)
        {
            var entity = ViewFactory.CreateInWorld(FieldConfig.View.ViewPrefab, position).Entity;
            return entity
                    .Add<Hoverable>()
                ;
        }
    }
}
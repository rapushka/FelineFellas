using UnityEngine;

namespace FelineFellas
{
    public interface IViewFactory : IService
    {
        void Initialize();

        GameEntityBehaviour CreateInWorld(GameEntityBehaviour prefab, Vector2 position);
    }

    public class ViewFactory : IViewFactory
    {
        private Transform _worldRoot;

        public void Initialize()
        {
            _worldRoot = new GameObject("[Entity Behaviours]").transform;
        }

        public GameEntityBehaviour CreateInWorld(GameEntityBehaviour prefab, Vector2 position)
            => CreateBehaviour(prefab, position, _worldRoot);

        private static GameEntityBehaviour CreateBehaviour(GameEntityBehaviour prefab, Vector2 position, Transform parent)
        {
            var entity = CreateEntity.Empty();
            var view = Object.Instantiate(prefab, parent);
            view.transform.position = position;
            view.Register(entity);

            entity
                .Add<View, GameEntityBehaviour>(view)
                .Add<WorldPosition, Vector2>(position)
                ;

            return view;
        }
    }
}
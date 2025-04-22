using Entitas;
using UnityEngine;

namespace FelineFellas
{
    public sealed class SpawnGridSystem : IInitializeSystem
    {
        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        public void Initialize()
        {
            var size = GameConfig.Field.FieldSize;
            Debug.Log($"TODO: spawn grid with sizes {size}");
        }
    }
}
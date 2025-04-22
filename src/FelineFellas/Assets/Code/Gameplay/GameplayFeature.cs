using Entitas;
using UnityEngine;

namespace FelineFellas
{
    public sealed class GameplayFeature : Feature
    {
        public GameplayFeature()
            : base(nameof(GameplayFeature))
        {
            Add(new GreetingSystem());
        }
    }

    public sealed class GreetingSystem : IInitializeSystem
    {
        public void Initialize()
        {
            Debug.Log("hello from ecs world!");
        }
    }
}
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class BoilerplateFeature : Feature
    {
        public BoilerplateFeature() : base(nameof(BoilerplateFeature))
        {
            var contexts = Contexts.Instance;

            Add(new SelfEventSystem<GameScope, WorldPosition>(contexts));
            Add(new SelfFlagEventSystem<GameScope, Hovered>(contexts));
            Add(new SelfEventSystem<GameScope, SpriteSortingIndex>(contexts));
            Add(new SelfEventSystem<GameScope, Rotation>(contexts));
            Add(new SelfEventSystem<GameScope, Scale>(contexts));
            Add(new SelfEventSystem<GameScope, CardTitle>(contexts));
            Add(new SelfEventSystem<GameScope, CardIcon>(contexts));
        }
    }
}
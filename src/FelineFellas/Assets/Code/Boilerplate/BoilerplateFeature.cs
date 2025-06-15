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
            Add(new SelfEventSystem<GameScope, RenderOrderIndex>(contexts));
            Add(new SelfEventSystem<GameScope, Rotation>(contexts));
            Add(new SelfEventSystem<GameScope, Scale>(contexts));
            Add(new SelfEventSystem<GameScope, CardTitle>(contexts));
            Add(new SelfEventSystem<GameScope, CardIcon>(contexts));
            Add(new SelfEventSystem<GameScope, MaxHealth>(contexts));
            Add(new SelfEventSystem<GameScope, Health>(contexts));
            Add(new SelfEventSystem<GameScope, Strength>(contexts));
            Add(new SelfEventSystem<GameScope, Price>(contexts));
            Add(new SelfEventSystem<GameScope, Money>(contexts));
            Add(new SelfFlagEventSystem<GameScope, Interactable>(contexts));
            Add(new SelfEventSystem<GameScope, CardFace>(contexts));
            Add(new SelfFlagEventSystem<GameScope, UseLimitReached>(contexts));
        }
    }
}
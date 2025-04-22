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
        }
    }
}
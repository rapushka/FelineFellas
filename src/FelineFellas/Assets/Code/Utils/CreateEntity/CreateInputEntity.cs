using Entitas.Generic;

namespace FelineFellas
{
    public static class CreateInputEntity
    {
        public static Entity<InputScope> Empty()
            => Contexts.Instance.Get<InputScope>().CreateEntity();
    }
}
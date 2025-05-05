using Entitas.Generic;

namespace FelineFellas
{
    public sealed class ID : PrimaryIndexComponent<EntityID>, IInScope<GameScope> { }
}
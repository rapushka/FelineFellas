using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CardFace : ValueComponent<Face>, IInScope<GameScope>, IEvent<Self> { }
}
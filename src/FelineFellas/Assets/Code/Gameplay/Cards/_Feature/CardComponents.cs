using Entitas.Generic;

namespace FelineFellas
{
    public sealed class Card : ValueComponent<CardIDRef>, IInScope<GameScope> { }
}
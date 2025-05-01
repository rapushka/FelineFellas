using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public sealed class CardTitle : ValueComponent<string>, IInScope<GameScope>, IEvent<Self> { }

    public sealed class CardIcon : ValueComponent<Sprite>, IInScope<GameScope>, IEvent<Self> { }
}
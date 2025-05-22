using Entitas.Generic;

namespace FelineFellas
{
    public sealed class EnemyTurn : ValueComponent<float>, IInScope<GameScope> { }

    public sealed class EnemyTurnEnded : FlagComponent, IInScope<GameScope> { }
}
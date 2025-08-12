using Entitas.Generic;

namespace FelineFellas
{
    public sealed class Map : FlagComponent, IInScope<GameScope> { }

    public sealed class StartFightWithNextEnemyLead : FlagComponent, IInScope<GameScope> { }

    /// Event -> Enemy Lead
    public sealed class StartFightEvent : ValueComponent<EntityID>, IInScope<GameScope> { }

    public sealed class NextEnemyLead : FlagComponent, IInScope<GameScope> { }
}
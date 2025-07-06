using Entitas.Generic;

namespace FelineFellas
{
#region Static Data
    public sealed class EventCard : FlagComponent, IInScope<GameScope> { }

    /// A Card without specific target, used simply on the game field and gives some global effect.
    /// Only Event Cards can be Global
    public sealed class TargetGlobal : FlagComponent, IInScope<GameScope> { }

    /// Orders and Events
    public sealed class DiscardAfterUse : FlagComponent, IInScope<GameScope> { }

    public sealed class CanUseOnlyOnOurRow : FlagComponent, IInScope<GameScope> { }
#endregion

#region State
    public sealed class WillBeUsed : FlagComponent, IInScope<GameScope> { }

    /// Dragged Card -> Other Card/Cell on which this card will be Used/Placed
    public sealed class DropCardOn : ValueComponent<EntityID>, IInScope<GameScope> { }
#endregion
}
using Entitas.Generic;

namespace FelineFellas
{
#region Static Data
    public sealed class AbilityConfigRef : ValueComponent<AbilityConfig>, IInScope<GameScope> { }

    /// Ability -> Card
    public sealed class AbilityTemplate : IndexComponent<EntityID>, IInScope<GameScope> { }

    /// Ability -> Card.
    /// It's a temporary instance of the ability,
    /// that used to mutate it due to other effects
    /// and will be destroyed after used.
    public sealed class AbilityUse : IndexComponent<EntityID>, IInScope<GameScope> { }

#region Target Selection
    /// Target Subject (Target) - the card, which used the Ability
    public sealed class TargetSubject : ValueComponent<EntityID>, IInScope<GameScope> { }

    /// Target Object (Sender) - the card, which will be affected by the Ability
    public sealed class TargetObject : ValueComponent<EntityID>, IInScope<GameScope> { }

#region Target Object
    public sealed class TargetObjectAsFreeCell : ValueComponent<CellDirection>, IInScope<GameScope> { }

    public sealed class TargetObjectAsOpponent : FlagComponent, IInScope<GameScope> { }

    public sealed class TargetObjectAsSelf : FlagComponent, IInScope<GameScope> { }
#endregion
#endregion

#region Ability Types
    public sealed class AbilityAttack : ValueComponent<float>, IInScope<GameScope> { }

    public sealed class AbilityMove : FlagComponent, IInScope<GameScope> { }

    public sealed class AbilitySendToDiscard : FlagComponent, IInScope<GameScope> { }
#endregion

#region Triggers
    public sealed class TriggerOnUse : FlagComponent, IInScope<GameScope> { }
#endregion
#endregion
}
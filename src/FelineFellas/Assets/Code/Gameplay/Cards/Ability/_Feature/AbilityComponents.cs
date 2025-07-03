using Entitas.Generic;

namespace FelineFellas
{
    /// Ability -> Card
    public sealed class AbilityOf : ValueComponent<EntityID>, IInScope<GameScope> { }

    public sealed class Using : FlagComponent, IInScope<GameScope> { }

    public sealed class TargetObjectAsCell : ValueComponent<CellDirection>, IInScope<GameScope> { }

    public sealed class TargetObjectAsOpponent : FlagComponent, IInScope<GameScope> { }

    public sealed class TargetCellMustBeFree : FlagComponent, IInScope<GameScope> { }

    // Ability Types

    public sealed class AbilityAttack : ValueComponent<float>, IInScope<GameScope> { }

    public sealed class AbilityMove : FlagComponent, IInScope<GameScope> { }

    public sealed class AbilitySendToDiscard : FlagComponent, IInScope<GameScope> { }

    /// Sender
    public sealed class TargetSubject : ValueComponent<EntityID>, IInScope<GameScope> { }

    /// Receiver
    public sealed class TargetObject : ValueComponent<EntityID>, IInScope<GameScope> { }

    // Triggers
    public sealed class TriggerOnUse : FlagComponent, IInScope<GameScope> { }
}
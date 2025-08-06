using Entitas.Generic;

namespace FelineFellas
{
    public sealed class TurnMediator : FlagComponent, IInScope<GameScope>, IUnique { }

    public sealed class ToNextTurnState : FlagComponent, IInScope<GameScope> { }

    public sealed class ChangeStateAfter : ValueComponent<float>, IInScope<GameScope> { }

#region States
    public sealed class OnPlayerTurnStartedState : FlagComponent, IInScope<GameScope> { }

    public sealed class InDrawCardsState : FlagComponent, IInScope<GameScope> { }

    public sealed class InPlayerTurnState : FlagComponent, IInScope<GameScope> { }

    public sealed class OnPlayerTurnEndedState : FlagComponent, IInScope<GameScope> { }

    public sealed class OnEnemyTurnStartedState : FlagComponent, IInScope<GameScope> { }

    public sealed class InEnemyTurnState : FlagComponent, IInScope<GameScope> { }

    public sealed class OnEnemyTurnEndedState : FlagComponent, IInScope<GameScope> { }
#endregion
}
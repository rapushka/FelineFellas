using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class ResetStaminaOnTurnEnded<TTurnEndEven, TSideCard> : IExecuteSystem
        where TTurnEndEven : FlagComponent, IInScope<GameScope>, new()
        where TSideCard : FlagComponent, IInScope<GameScope>, new()
    {
        private readonly IGroup<Entity<GameScope>> _event
            = GroupBuilder<GameScope>
                .With<TTurnEndEven>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _cards
            = GroupBuilder<GameScope>
                .With<OutOfStamina>()
                .And<TSideCard>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(32);

        public void Execute()
        {
            foreach (var _ in _event)
            foreach (var card in _cards.GetEntities(_buffer))
            {
                card.Is<OutOfStamina>(false);
            }
        }
    }
}
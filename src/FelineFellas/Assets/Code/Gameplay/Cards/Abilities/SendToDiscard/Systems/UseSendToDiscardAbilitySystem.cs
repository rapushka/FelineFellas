using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UseSendToDiscardAbilitySystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _cards
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<Used>()
                .And<UseTarget>()
                .And<AbilitySendToDiscard>()
                .Build();

        public void Execute()
        {
            foreach (var card in _cards)
            {
                var target = card.Get<UseTarget>().Value.GetEntity();
                CardUtils.Discard(target);
            }
        }
    }
}
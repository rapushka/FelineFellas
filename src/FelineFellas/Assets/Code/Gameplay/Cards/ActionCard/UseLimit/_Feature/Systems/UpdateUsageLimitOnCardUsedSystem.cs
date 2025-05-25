using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UpdateUsageLimitOnCardUsedSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _cards
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<ActionCard>()
                .And<Used>()
                .Build();

        public void Execute()
        {
            foreach (var card in _cards)
            {
                var targetUnit = card.Get<UseTarget>().Value.GetEntity();

                targetUnit.Is<UseLimitReached>(true);
            }
        }
    }
}
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class SendRecalculateIndexesRequestOnCardUsedSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _usedCard
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<Used>()
                .Build();

        public void Execute()
        {
            foreach (var _ in _usedCard)
            {
                CreateEntity.Empty()
                    .Add<RecalculateInHandIndexes>();
            }
        }
    }
}
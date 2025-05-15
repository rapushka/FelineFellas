using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class SendRecalculateIndexesOnCardSoldSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _droppedCards
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<WillBeSold>()
                .And<Dropped>()
                .Build();

        public void Execute()
        {
            foreach (var _ in _droppedCards)
            {
                CreateEntity.Empty()
                    .Add<RecalculateInHandIndexes>();
            }
        }
    }
}
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class RecalculateCardsIndexesInHandOnRequestSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _requests
            = GroupBuilder<GameScope>
                .With<RecalculateInHandIndexes>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _leftCards
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<InHandIndex>()
                .Build();

        public void Execute()
        {
            var counter = 0;

            foreach (var request in _requests)
            foreach (var card in _leftCards)
            {
                card.Set<InHandIndex, int>(counter++);

                request.Is<Destroy>(true);
            }
        }
    }
}
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

        private readonly IGroup<Entity<GameScope>> _actors
            = GroupBuilder<GameScope>
                .With<Actor>()
                .And<OnSide>()
                .Build();

        public void Execute()
        {
            foreach (var request in _requests)
            foreach (var actor in _actors)
            {
                var counter = 0;
                foreach (var card in _leftCards.Where(actor.OnSameSide))
                    card.Set<InHandIndex, int>(counter++);

                request.Is<Destroy>(true);
            }
        }
    }
}
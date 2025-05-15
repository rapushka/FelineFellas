using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class DestroySoldCardSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _droppedCards
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<WillBeSold>()
                .And<Dropped>()
                .Build();

        public void Execute()
        {
            foreach (var card in _droppedCards)
            {
                card.Add<Destroy>();
            }
        }
    }
}
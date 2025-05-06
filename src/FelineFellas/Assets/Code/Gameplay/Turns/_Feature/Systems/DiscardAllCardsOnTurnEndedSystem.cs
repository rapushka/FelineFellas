using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class DiscardAllCardsOnTurnEndedSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<EndTurnEvent>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _cardsInHand
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<InHandIndex>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(16);

        public void Execute()
        {
            foreach (var _ in _events)
            foreach (var card in _cardsInHand.GetEntities(_buffer))
            {
                CardUtils.Discard(card);
            }
        }
    }
}
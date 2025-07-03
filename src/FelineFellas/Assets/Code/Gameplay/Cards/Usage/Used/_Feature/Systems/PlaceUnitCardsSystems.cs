using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class PlaceUnitCardsSystems : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _droppedCards
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<UnitCard>()
                .And<DropCardOn>()
                .And<WillBeUsed>()
                .And<Dropped>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(8);

        public void Execute()
        {
            foreach (var card in _droppedCards.GetEntities(_buffer))
            {
                var cell = card.Get<DropCardOn>().Value.GetEntity();

                CardUtils.PlaceCardOnField(card, cell)
                    .Chain(CardUtils.MarkUsed)
                    ;
            }
        }
    }
}
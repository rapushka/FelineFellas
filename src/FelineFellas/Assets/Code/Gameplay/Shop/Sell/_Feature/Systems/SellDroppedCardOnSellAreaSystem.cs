using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class SellDroppedCardOnSellAreaSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _droppedCards
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<WillBeSold>()
                .And<Dropped>()
                .Build();

        private readonly List<Entity<GameScope>> _buffer = new(8);

        private static Entity<GameScope> Player => Contexts.Instance.Get<GameScope>().Unique.GetEntity<Player>();

        public void Execute()
        {
            foreach (var card in _droppedCards.GetEntities(_buffer))
            {
                var fullPrice = card.Get<Price>().Value;

                Player.Increment<Money>(ShopUtils.PriceToSell(fullPrice));

                card.Add<Destroy>();
            }
        }
    }
}
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class GainMoneyForSoldCardSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _droppedCards
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<WillBeSold>()
                .And<Dropped>()
                .Build();

        private static Entity<GameScope> Player => Contexts.Instance.Get<GameScope>().Unique.GetEntity<Player>();

        public void Execute()
        {
            foreach (var card in _droppedCards)
            {
                var fullPrice = card.Get<Price>().Value;
                Player.Increment<Money>(ShopUtils.PriceToSell(fullPrice));
            }
        }
    }
}
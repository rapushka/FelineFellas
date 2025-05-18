using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class DrawCardsFromDeckSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<DrawCardsEvent>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _cardsInDeck
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<CardInDeck>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _cardsInHand
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<InHandIndex>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _players
            = GroupBuilder<GameScope>
                .With<Player>()
                .And<HandSize>()
                .Build();

        private static IRandomService RandomService => ServiceLocator.Resolve<IRandomService>();

        public void Execute()
        {
            foreach (var _ in _events)
            foreach (var player in _players)
            {
                var handSize = player.Get<HandSize>().Value;

                while (_cardsInHand.count < handSize && _cardsInDeck.Any())
                {
                    var card = RandomService.PickRandom(_cardsInDeck);
                    CardUtils.DrawCardToHand(card, _cardsInHand.count);
                }
            }
        }
    }
}
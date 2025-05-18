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

        private readonly IGroup<Entity<GameScope>> _actors
            = GroupBuilder<GameScope>
                .With<Actor>()
                .And<HandSize>()
                .Build();

        private static IRandomService RandomService => ServiceLocator.Resolve<IRandomService>();

        public void Execute()
        {
            foreach (var _ in _events)
            foreach (var actor in _actors)
            {
                var handSize = actor.Get<HandSize>().Value;

                while (_cardsInHand.count < handSize && _cardsInDeck.Any(OnSameSide))
                {
                    var card = RandomService.PickRandom(_cardsInDeck.Where(OnSameSide));
                    CardUtils.DrawCardToHand(card, _cardsInHand.count);
                }

                continue;
                bool OnSameSide(Entity<GameScope> card) => actor.OnSameSide(card);
            }
        }
    }
}
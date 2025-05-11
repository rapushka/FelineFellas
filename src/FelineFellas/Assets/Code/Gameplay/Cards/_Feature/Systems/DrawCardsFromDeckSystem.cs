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

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static int HandSize => GameConfig.Cards.HandSize;

        public void Execute()
        {
            foreach (var _ in _events)
            {
                while (_cardsInHand.count < HandSize)
                {
                    var isCardDrawn = _cardsInDeck.TryGetFirst(out var card);

                    if (!isCardDrawn)
                        break;

                    card
                        .Remove<CardInDeck>()
                        .Add<InHandIndex, int>(_cardsInHand.count)
                        ;
                }
            }
        }
    }
}
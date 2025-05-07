using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class DrawCardsOnTurnStartSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _events
            = GroupBuilder<GameScope>
                .With<StartTurnEvent>()
                .Build();

        private readonly IGroup<Entity<GameScope>> _cardsInDeck
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<InDeck>()
                .Build();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        public void Execute()
        {
            foreach (var _ in _events)
            {
                for (var i = 0; i < GameConfig.Cards.HandSize; i++)
                {
                    var card = _cardsInDeck.First();
                    card
                        .Is<InDeck>(false)
                        .Add<InHandIndex, int>(i)
                        ;
                }
            }
        }
    }
}
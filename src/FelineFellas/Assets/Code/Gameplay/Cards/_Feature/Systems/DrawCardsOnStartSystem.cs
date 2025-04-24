using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class DrawCardsOnStartSystem : IInitializeSystem
    {
        private readonly IGroup<Entity<GameScope>> _cardsInDeck
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<InDeck>()
                .Build();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        public void Initialize()
        {
            for (var i = 0; i < GameConfig.Cards.HandSize; i++)
            {
                var card = _cardsInDeck.First();
                card
                    .Is<InDeck>(false)
                    .Is<InHand>(true)
                    ;
            }
        }
    }
}
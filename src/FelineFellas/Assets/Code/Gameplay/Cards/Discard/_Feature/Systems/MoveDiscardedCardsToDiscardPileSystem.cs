using System.Collections.Generic;
using Entitas;
using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public sealed class MoveDiscardedCardsToDiscardPileSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _discardedCards
            = GroupBuilder<GameScope>
                .With<SendToDiscard>()
                .Build();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private readonly List<Entity<GameScope>> _buffer = new(32);

        public void Execute()
        {
            foreach (var card in _discardedCards.GetEntities(_buffer))
            {
                var side = card.Get<OnSide>().Value;
                var targetPosition = side.Visit(
                    onPlayer: () => GameConfig.Layout.PlayerDiscard,
                    onEnemy: () => GameConfig.Layout.EnemyDiscard
                );

                card
                    .Set<TargetPosition, Vector2>(targetPosition)
                    .Is<InDiscard>(true)
                    .Is<SendToDiscard>(false)
                    ;
            }
        }
    }
}
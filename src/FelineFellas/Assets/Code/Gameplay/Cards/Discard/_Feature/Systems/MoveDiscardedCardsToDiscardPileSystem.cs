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
                card
                    .Set<TargetPosition, Vector2>(GameConfig.Layout.PlayerDiscard)
                    .Is<InDiscard>(true)
                    .Is<SendToDiscard>(false)
                    ;
            }
        }
    }
}
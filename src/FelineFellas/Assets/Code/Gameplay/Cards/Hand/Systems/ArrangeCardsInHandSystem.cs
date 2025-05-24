using Entitas;
using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public class ArrangeCardsInHandSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _actors
            = GroupBuilder<GameScope>
                .With<Actor>()
                .Build();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static CardsConfig.ViewConfig ViewConfig => GameConfig.Cards.View;

        public void Execute()
        {
            foreach (var actor in _actors)
                ArrangeCardsForActor(actor);
        }

        private void ArrangeCardsForActor(Entity<GameScope> actor)
        {
            var side = actor.Get<OnSide>().Value;

            var handCenter = side.Visit(
                onPlayer: () => GameConfig.Layout.PlayerHand,
                onEnemy: () => GameConfig.Layout.EnemyHand
            );
            var additionalAngle = side.Visit(
                onPlayer: () => 0f,
                onEnemy: () => 180f
            );
            var offsetDirection = side.Visit(
                onPlayer: () => 1f,
                onEnemy: () => -1f
            );

            var cardsInHand = ActorUtils.GetCardsInHand(actor);
            var cardCount = cardsInHand.count;

            if (cardCount == 1)
            {
                var singleCard = cardsInHand.GetSingleEntity();

                singleCard.Set<TargetPosition, Vector2>(handCenter);
                singleCard.Set<TargetRotation, float>(0 + additionalAngle);
                return;
            }

            var totalAngle = (cardCount * 3f).Clamp(max: ViewConfig.MaxCardAngle);
            var angleStep = totalAngle / (cardCount - 1);
            var startAngle = -totalAngle / 2f;

            foreach (var card in cardsInHand)
            {
                var index = card.Get<InHandIndex>().Value;

                var currentAngle = startAngle + index * angleStep;
                var radAngle = (currentAngle + additionalAngle).ToRadians();

                var handRadius = ViewConfig.HandRadius;
                var targetPosition = new Vector2(
                    x: Mathf.Sin(radAngle) * handRadius,
                    y: Mathf.Abs(currentAngle) * 0.01f * ViewConfig.VerticalOffset * offsetDirection
                    // z: -Mathf.Cos(radAngle) * handRadius + handRadius
                );

                targetPosition += handCenter;

                card.Set<TargetPosition, Vector2>(targetPosition);
                card.Set<TargetRotation, float>(-currentAngle + additionalAngle);
            }
        }
    }
}
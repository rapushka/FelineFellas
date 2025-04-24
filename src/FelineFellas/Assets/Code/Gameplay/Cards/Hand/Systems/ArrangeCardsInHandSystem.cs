using Entitas;
using Entitas.Generic;
using UnityEngine;

namespace FelineFellas
{
    public class ArrangeCardsInHandSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _cardsInHand
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<InHandIndex>()
                .And<WorldPosition>()
                .Without<Dragging>()
                .Build();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static CardsConfig.ViewConfig ViewConfig => GameConfig.Cards.View;

        public void Execute()
        {
            var totalAngle = (_cardsInHand.count * 3f).Clamp(max: ViewConfig.MaxCardAngle);
            var angleStep = totalAngle / (_cardsInHand.count - 1);
            var startAngle = -totalAngle / 2f;

            var handCenter = ViewConfig.HandCenter;

            foreach (var card in _cardsInHand)
            {
                var index = card.Get<InHandIndex>().Value;

                var currentAngle = startAngle + index * angleStep;
                var radAngle = currentAngle.ToRadians();

                var handRadius = ViewConfig.HandRadius;
                var targetPosition = new Vector2(
                    x: Mathf.Sin(radAngle) * handRadius,
                    y: Mathf.Abs(currentAngle) * 0.01f * ViewConfig.VerticalOffset
                    // z: -Mathf.Cos(radAngle) * handRadius + handRadius
                );

                targetPosition += handCenter;

                card.Set<TargetPosition, Vector2>(targetPosition);
                card.Set<TargetRotation, float>(-currentAngle);
            }
        }
    }
}
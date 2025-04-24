using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CalculateCardViewScaleSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _cards
            = GroupBuilder<GameScope>
                .With<Card>()
                .Build();

        public void Execute()
        {
            foreach (var card in _cards)
            {
                var isInHand = card.Has<InHandIndex>();
                if (!isInHand)
                {
                    card.Set<TargetScale, float>(1f);
                    continue;
                }

                var totalScale = 2f * (card.Is<Hovered>() ? 2f : 1f);
                card.Set<TargetScale, float>(totalScale);
            }
        }
    }
}
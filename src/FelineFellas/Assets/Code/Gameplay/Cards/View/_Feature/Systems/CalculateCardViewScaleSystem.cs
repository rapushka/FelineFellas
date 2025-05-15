using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class CalculateCardViewScaleSystem : IExecuteSystem
    {
        private const float DefaultScale = 1f;

        private readonly IGroup<Entity<GameScope>> _cards
            = GroupBuilder<GameScope>
                .With<Card>()
                .Build();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static CardsConfig.ViewConfig ViewConfig => GameConfig.Cards.View;

        public void Execute()
        {
            foreach (var card in _cards)
            {
                var isInHand = card.Has<InHandIndex>();
                if (!isInHand)
                {
                    card.Set<TargetScale, float>(DefaultScale);
                    continue;
                }

                if (card.Is<WillBeUsed>() || card.Is<WillBeSold>())
                {
                    card.Set<TargetScale, float>(DefaultScale);
                    continue;
                }

                var totalScale = ViewConfig.CardInHandScaleUp
                    * (card.Is<Hovered>() ? ViewConfig.HoveredCardScaleUp : DefaultScale);

                card.Set<TargetScale, float>(totalScale);
            }
        }
    }
}
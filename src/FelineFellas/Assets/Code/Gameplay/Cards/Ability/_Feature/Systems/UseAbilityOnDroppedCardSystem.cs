using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class UseAbilityOnDroppedCardSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _cards
            = GroupBuilder<GameScope>
                .With<Card>()
                .And<Used>()
                .Build();

        public void Execute()
        {
            foreach (var card in _cards)
            {
                var triggeredAbilities = AbilityUtils.GetAbilitiesOfCard(card.ID()).With<TriggerOnUse>();
                foreach (var abilityTemplate in triggeredAbilities)
                {
                    var dropTargetID = card.Get<DropCardOn, EntityID>();
                    var abilityUse = AbilityUtils.Use(abilityTemplate);

                    // Event Cards can only have TargetObject, or be Global
                    if (card.Is<EventCard>() && !card.Is<TargetGlobal>())
                        abilityUse.Set<TargetObject, EntityID>(dropTargetID);

                    // Order Cards Always have TargetSubject
                    if (card.Is<OrderCard>())
                        abilityUse.Set<TargetSubject, EntityID>(dropTargetID);
                }
            }
        }
    }
}
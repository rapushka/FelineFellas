using System.Collections.Generic;
using System.Linq;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public static class AbilityUtils
    {
        private static EntityIndex<GameScope, AbilityTemplate, EntityID> AbilitiesOfCardsIndex
            => Contexts.Instance.Get<GameScope>().GetIndex<AbilityTemplate, EntityID>();

        private static EntityIndex<GameScope, AbilityUse, EntityID> AbilitiesUsageIndex
            => Contexts.Instance.Get<GameScope>().GetIndex<AbilityUse, EntityID>();

        public static IEnumerable<Entity<GameScope>> GetAbilityUsagesOfCard(EntityID cardID)
            => AbilitiesUsageIndex.GetEntities(cardID);

        public static IEnumerable<Entity<GameScope>> GetAbilitiesOfCardWith<TComponent>(EntityID cardID)
            where TComponent : IComponent, IInScope<GameScope>, new()
            => GetAbilitiesOfCard(cardID).Where(a => a.Has<TComponent>());

        public static IEnumerable<Entity<GameScope>> GetAbilitiesOfCard(EntityID cardID)
            => AbilitiesOfCardsIndex.GetEntities(cardID);

        /// Creates a Copy of the Ability for the single-use
        public static Entity<GameScope> Use(Entity<GameScope> abilityOriginal)
        {
            var config = abilityOriginal.Get<AbilityConfigRef>().Value;
            var cardID = abilityOriginal.Get<AbilityTemplate>().Value;

            return CreateEntity.Empty()
                    .Add<Name, string>("usage of ability")
                    .Add<AbilityUse, EntityID>(cardID)
                    .Chain(a => Assign(a, config))
                ;
        }

        public static Entity<GameScope> Assign(Entity<GameScope> ability, AbilityConfig config)
        {
            var isMove = config.TypeID is AbilityTypeID.Move;
            var isAttack = config.TypeID is AbilityTypeID.Attack;
            var isSendToDiscard = config.TypeID is AbilityTypeID.SendToDiscard;

            var targetDirection = config.TargetObject is TargetObjectTypeID.FreeCell;
            var targetOpponent = config.TargetObject is TargetObjectTypeID.Opponent;
            var targetSelf = config.TargetObject is TargetObjectTypeID.Self;

            return ability
                    .Add<AbilityConfigRef, AbilityConfig>(config)
                    .Is<AbilityMove>(isMove)
                    .Add<AbilityAttack, float>(config.Value, @if: isAttack)
                    .Is<AbilitySendToDiscard>(isSendToDiscard)
                    .Is<TargetObjectAsOpponent>(targetOpponent)
                    .Add<TargetObjectAsFreeCell, CellDirection>(config.Direction, @if: targetDirection)
                    .Is<TargetObjectAsSelf>(targetSelf)
                ;
        }
    }
}
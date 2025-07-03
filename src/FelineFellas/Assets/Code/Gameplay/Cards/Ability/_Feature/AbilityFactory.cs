using Entitas.Generic;

namespace FelineFellas
{
    public interface IAbilityFactory : IService
    {
        Entity<GameScope> Create(EntityID cardID, AbilityConfig config);
    }

    public class AbilityFactory : IAbilityFactory
    {
        public Entity<GameScope> Create(EntityID cardID, AbilityConfig config)
        {
            var isMove = config.Type is AbilityConfig.AbilityType.Move;
            var isAttack = config.Type is AbilityConfig.AbilityType.Attack;
            var isSendToDiscard = config.Type is AbilityConfig.AbilityType.SendToDiscard;

            var selectTargetAsDirection = config.TargetObject is AbilityConfig.TargetObjectSelectionType.FreeCell;
            var targetClosestOpponent = config.TargetObject is AbilityConfig.TargetObjectSelectionType.Opponent;

            return CreateEntity.Empty()
                    .Add<Name, string>("ability")
                    .Add<AbilityOf, EntityID>(cardID)
                    .Is<AbilityMove>(isMove)
                    .Add<AbilityAttack, float>(config.Value, @if: isAttack)
                    .Is<AbilitySendToDiscard>(isSendToDiscard)
                    .Is<TargetObjectAsOpponent>(targetClosestOpponent)
                    .Add<TargetObjectAsCell, CellDirection>(config.Direction, @if: selectTargetAsDirection)
                ;
        }
    }
}
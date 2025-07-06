namespace FelineFellas
{
    public sealed class UseCardAbilitiesFeature : Feature
    {
        public UseCardAbilitiesFeature()
            : base(nameof(UseCardAbilitiesFeature))
        {
            Add(new UseAbilityOnDroppedCardSystem());

            Add(new SelectOpponentForTargetObjectSystem());
            Add(new SelectSelfForTargetObjectSystem());
            Add(new SelectFreeCellForTargetObjectSystem());

            Add(new UseAttackAbilitySystem());
            Add(new UseMoveUnitAbilitySystem());
            Add(new UseSendToDiscardAbilitySystem());

            Add(new UpdateStaminaOnCardUsedSystem());

            Add(new DestroyAbilityUsagesSystem());
        }
    }
}
namespace FelineFellas
{
    public sealed class UseCardAbilitiesFeature : Feature
    {
        public UseCardAbilitiesFeature()
            : base(nameof(UseCardAbilitiesFeature))
        {
            Add(new CalculateOpponentSystem());

            Add(new UseAbilityOnDroppedCardSystem());

            Add(new SelectSelfForTargetObjectSystem());
            Add(new SelectFreeCellForTargetObjectSystem());

            Add(new UseAttackAbilitySystem());
            Add(new UseMoveUnitAbilitySystem());
            Add(new UseSendToDiscardAbilitySystem());

            Add(new UpdateUsageLimitOnCardUsedSystem());

            Add(new DestroyAbilityUsagesSystem());
        }
    }
}
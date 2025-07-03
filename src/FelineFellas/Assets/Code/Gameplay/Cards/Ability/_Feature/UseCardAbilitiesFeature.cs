namespace FelineFellas
{
    public sealed class UseCardAbilitiesFeature : Feature
    {
        public UseCardAbilitiesFeature()
            : base(nameof(UseCardAbilitiesFeature))
        {
            Add(new RequestClosestOpponentSystem());

            Add(new UseAbilityOnDroppedCardSystem());

            Add(new SelectSelfForTargetObjectSystem());

            Add(new UseAttackAbilitySystem());
            Add(new UseDirectionalMoveUnitAbilitySystem());
            Add(new UseSendToDiscardAbilitySystem());

            Add(new UpdateUsageLimitOnCardUsedSystem());

            Add(new DestroyAbilityUsagesSystem());
        }
    }
}
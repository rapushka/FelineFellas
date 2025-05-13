namespace FelineFellas
{
    public sealed class UseCardAbilitiesFeature : Feature
    {
        public UseCardAbilitiesFeature()
            : base(nameof(UseCardAbilitiesFeature))
        {
            Add(new RequestClosestOpponentSystem());

            Add(new UseAttackAbilitySystem());
            Add(new UseDirectionalMoveUnitAbilitySystem());
        }
    }
}
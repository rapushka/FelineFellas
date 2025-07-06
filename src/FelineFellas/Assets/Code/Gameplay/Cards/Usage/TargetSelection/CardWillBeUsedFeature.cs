namespace FelineFellas
{
    public sealed class CardWillBeUsedFeature : Feature
    {
        public CardWillBeUsedFeature()
            : base(nameof(CardWillBeUsedFeature))
        {
            Add(new ResetCardWillBeUsed());

            Add(new CheckGlobalCardUseSystem());
            Add(new CheckUnitCardUseSystem());
            Add(new CheckOrderUseOnUnitSystem());
        }
    }
}
namespace FelineFellas
{
    public sealed class ShopFeature : Feature
    {
        public ShopFeature()
            : base(nameof(ShopFeature))
        {
            Add(new CreateShopViewSystem());
        }
    }
}
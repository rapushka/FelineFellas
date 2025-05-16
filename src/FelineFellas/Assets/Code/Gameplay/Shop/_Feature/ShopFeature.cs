namespace FelineFellas
{
    public sealed class ShopFeature : Feature
    {
        public ShopFeature()
            : base(nameof(ShopFeature))
        {
            Add(new CreateShopViewSystem());

            Add(new CheckSellDraggingCardSystem());

            Add(new FillEmptyShopSlotsSystem());

            Add(new UpdateCanBuyPlacedCardSystem());
            Add(new SetCanNotBuyIfNoCardSystem());
            Add(new UpdateCanBuyInteractableSystem());

            Add(new GainMoneyForSoldCardSystem());
            Add(new DestroySoldCardSystem());
            Add(new SendRecalculateIndexesOnCardSoldSystem());
        }
    }
}
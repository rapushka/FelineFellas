namespace FelineFellas
{
    public sealed class CardsFeature : Feature
    {
        public CardsFeature()
            : base(nameof(CardsFeature))
        {
            Add(new SpawnDeckSystem());
            Add(new DrawCardsOnStartSystem());

            // View
            Add(new ArrangeCardsInHandSystem());
            Add(new CalculateCardViewScaleSystem());
        }
    }
}
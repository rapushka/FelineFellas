using Entitas;

namespace FelineFellas
{
    public sealed class CreateShopViewSystem : IInitializeSystem
    {
        private static IShopFactory ShopFactory => ServiceLocator.Resolve<IShopFactory>();

        public void Initialize()
        {
            ShopFactory.Create();
        }
    }
}
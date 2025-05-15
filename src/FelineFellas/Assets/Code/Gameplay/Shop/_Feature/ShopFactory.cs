using Entitas.Generic;

namespace FelineFellas
{
    public interface IShopFactory : IService
    {
        Entity<GameScope> Create();
    }

    public class ShopFactory : IShopFactory
    {
        private static IViewFactory ViewFactory => ServiceLocator.Resolve<IViewFactory>();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static MoneyConfig.ShopViewConfig Config => GameConfig.Money.ShopView;

        public Entity<GameScope> Create()
        {
            var shop = ViewFactory.CreateInWorld(Config.ShopPrefab, Config.ShopSpawnPosition).Entity
                    .Add<Shop>()
                ;

            return shop;
        }
    }
}
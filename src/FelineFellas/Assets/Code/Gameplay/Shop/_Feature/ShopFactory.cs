using Entitas.Generic;
using UnityEngine;

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

        private static MoneyConfig.ShopConfig     Config     => GameConfig.Money.Shop;
        private static MoneyConfig.ShopViewConfig ViewConfig => GameConfig.Money.ShopView;

        public Entity<GameScope> Create()
        {
            var shop = ViewFactory.CreateInWorld(ViewConfig.ShopPrefab, ViewConfig.ShopSpawnPosition).Entity
                    .Add<Name, string>("shop")
                    .Add<Shop>()
                ;

            var shopPosition = shop.WorldPosition();
            var totalSlots = Config.ItemSlotsInShop;
            var spacing = ViewConfig.SlotsSpacing;
            var startY = -((totalSlots - 1) * spacing) / 2f;

            for (var i = 0; i < totalSlots; i++)
            {
                var y = startY + i * spacing;
                CreateSlot(new Vector2(0, y) + shopPosition)
                    .Add<ShopSlot, EntityID>(shop.ID());
            }

            return shop;
        }

        private Entity<GameScope> CreateSlot(Vector2 position)
            => ViewFactory.CreateInWorld(ViewConfig.ShopSlotPrefab, position).Entity
                .Add<Name, string>("shop slot")
                .Add<Empty>()
                .Is<CanBuy>(false);
    }
}
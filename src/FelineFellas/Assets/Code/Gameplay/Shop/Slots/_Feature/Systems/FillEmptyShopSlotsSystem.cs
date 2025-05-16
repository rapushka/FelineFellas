using System.Collections.Generic;
using Entitas;
using Entitas.Generic;

namespace FelineFellas
{
    public sealed class FillEmptyShopSlotsSystem : IExecuteSystem
    {
        private readonly IGroup<Entity<GameScope>> _slots
            = GroupBuilder<GameScope>
                .With<ShopSlot>()
                .And<Empty>()
                .Build();

        private static IRandomService RandomService => ServiceLocator.Resolve<IRandomService>();

        private static IGameConfig GameConfig => ServiceLocator.Resolve<IGameConfig>();

        private static ICardFactory CardFactory => ServiceLocator.Resolve<ICardFactory>();

        private readonly List<Entity<GameScope>> _buffer = new(16);

        public void Execute()
        {
            foreach (var slot in _slots.GetEntities(_buffer))
            {
                var rarity = RandomService.PickRandom(GameConfig.Money.Shop.RarityWeights).Rarity;
                var configs = GameConfig.Cards.GetCardsOfRarity(rarity);

                var cardConfig = RandomService.PickRandom(configs);

                CardFactory.CreateCardInShop(cardConfig.ID, slot);
            }
        }
    }
}
using System.Linq;
using System.Text;
using Entitas.Generic;

namespace FelineFellas
{
    public class GameEntityFormatter : EntityStringBuilderFormatter<GameScope>
    {
        protected override void BuildName(ref StringBuilder stringBuilder, in Entity<GameScope> entity)
        {
            var buffer = new[]
            {
                entity.GetOrDefault<ID>()?.Value.ID.ToString() ?? "_",
                $"{entity.GetName()} |",

                entity.Has<CardInDeck>() ? "in-deck" : string.Empty,
                entity.Has<InHandIndex>() ? "in-hand" : string.Empty,
                entity.Has<InDiscard>() ? "in-discard" : string.Empty,
                entity.ToString<OnField, Coordinates>(prefix: "on-field: "),
                entity.Has<CardInShopSlot>() ? "in-shop" : string.Empty,

                entity.ToString<Enemy>(),
                entity.ToString<Fella>(),
                entity.ToString<Leader>(),

                ToStringHealth(entity),

                entity.ToString<Card, CardIDRef>(),
            };

            stringBuilder.AppendJoin(separator: "  ", buffer.Where(s => !s.IsEmpty()));
        }

        private static string ToStringHealth(in Entity<GameScope> entity)
        {
            if (entity.TryGet<Health, int>(out var health)
                && entity.TryGet<MaxHealth, int>(out var maxHealth))
                return $"HP: {health:### ###}/{maxHealth:### ###}";

            return string.Empty;
        }
    }
}
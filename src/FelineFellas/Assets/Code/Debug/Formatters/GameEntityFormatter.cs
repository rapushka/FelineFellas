using System.Linq;
using System.Text;
using Entitas.Generic;

namespace FelineFellas
{
    public class GameEntityFormatter : EntityStringBuilderFormatter<GameScope>
    {
        private const string EmptyString = "";

        protected override void BuildName(ref StringBuilder stringBuilder, in Entity<GameScope> entity)
        {
            var buffer = new[]
            {
                entity.GetOrDefault<ID>()?.Value.ID.ToString() ?? "_",
                $"{entity.GetName()} |",

                entity.Has<CardInDeck>() ? "in-deck" : EmptyString,
                entity.Has<InHandIndex>() ? "in-hand" : EmptyString,
                entity.Has<InDiscard>() ? "in-discard" : EmptyString,
                ToStringCardOnField(entity),
                entity.ToString<OnSide, Side>(prefix: "on-side:"),
                entity.Has<CardInShopSlot>() ? "in-shop" : EmptyString,

                entity.ToString<EnemyUnit>(),
                entity.ToString<Fella>(),
                entity.ToString<Leader>(),

                ToStringHealth(entity),

                entity.ToString<CardTitle, string>(),
            };

            stringBuilder.AppendJoin(separator: "  ", buffer.Where(s => !s.IsEmpty()));
        }

        private static string ToStringHealth(in Entity<GameScope> entity)
        {
            if (entity.TryGet<Health, int>(out var health)
                && entity.TryGet<MaxHealth, int>(out var maxHealth))
                return $"HP: {health:### ###}/{maxHealth:### ###}";

            return EmptyString;
        }

        private static string ToStringCardOnField(in Entity<GameScope> entity)
        {
            if (!entity.Is<OnField>())
                return EmptyString;

            var cell = entity.Parent();
            var cellIndex = cell.GetOrDefault<CellIndex>()?.Value.ToString() ?? "NaN";
            return $"on-field: [{cellIndex}]";
        }
    }
}
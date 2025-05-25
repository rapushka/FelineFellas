using System;
using Entitas.Generic;

namespace FelineFellas
{
    public enum Side
    {
        Unknown = 0,
        Player = 1,
        Enemy = 2,
    }

    public static class SideExtensions
    {
        public static Entity<GameScope> AssignToSide(this Entity<GameScope> card, Side side)
        {
            card.Set<OnSide, Side>(side);
            var isUnit = card.Is<UnitCard>();

            if (side is Side.Player)
            {
                card
                    .Add<PlayerCard>()
                    .Is<Fella>(isUnit && !card.Is<Leader>())
                    ;
            }

            if (side is Side.Enemy)
            {
                card
                    .Add<EnemyCard>()
                    .Is<EnemyUnit>(isUnit)
                    ;
            }

            return card;
        }

        public static bool OnSameSide(this Entity<GameScope> @this, Entity<GameScope> other)
            => @this.Get<OnSide>().Value == other.Get<OnSide>().Value;

        public static T Visit<T>(
            this Side @this,
            Func<T> onPlayer,
            Func<T> onEnemy,
            Func<T> onUnknown = null
        )
        {
            if (@this is Side.Unknown && onUnknown is not null)
                return onUnknown.Invoke();

            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault - kys
            return @this switch
            {
                Side.Player => onPlayer.Invoke(),
                Side.Enemy  => onEnemy.Invoke(),
                _           => throw new("Unknown Side!"),
            };
        }
    }
}
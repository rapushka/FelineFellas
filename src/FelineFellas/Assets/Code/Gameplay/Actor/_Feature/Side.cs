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
        public static Entity<GameScope> AddSideFlag(this Entity<GameScope> unit)
        {
            var side = unit.Get<OnSide>().Value;

            if (side is Side.Player && !unit.Is<Leader>())
                unit.Add<Fella>();

            if (side is Side.Enemy)
                unit.Add<EnemyUnit>();

            return unit;
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
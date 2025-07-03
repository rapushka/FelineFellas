using System;

namespace FelineFellas
{
    public static class ChainExtensions
    {
        public static T Chain<T>(this T @this, Func<T, T> action)
            => action.Invoke(@this);

        public static T Chain<T>(this T @this, Func<T, T> action, Func<T, bool> @if)
        {
            return @if.Invoke(@this)
                ? action.Invoke(@this)
                : @this;
        }

        public static T Chain<T>(this T @this, Func<T, T> action, bool @if)
        {
            return @if
                ? action.Invoke(@this)
                : @this;
        }
    }
}
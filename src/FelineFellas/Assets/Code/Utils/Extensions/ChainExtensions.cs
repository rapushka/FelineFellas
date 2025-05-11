using System;

namespace FelineFellas
{
    public static class ChainExtensions
    {
        public static T Chain<T>(this T @this, Func<T, T> action)
            => action.Invoke(@this);
    }
}
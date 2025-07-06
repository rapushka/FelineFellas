using System;

namespace FelineFellas
{
    [Serializable]
    [JetBrains.Annotations.UsedImplicitly]
    public enum CellDirection
    {
        Unknown = 0,
        Left = 1,
        Right = 2,
        Any = 3,
    }

    public static class CellDirectionExtensions
    {
        public static T Visit<T>(
            this CellDirection @this,
            Func<T> onLeft,
            Func<T> onRight,
            Func<T> onAny,
            Func<T> onUnknown = null
        )
        {
            if (@this is CellDirection.Unknown && onUnknown is not null)
                return onUnknown.Invoke();

            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault - kys
            return @this switch
            {
                CellDirection.Left  => onLeft.Invoke(),
                CellDirection.Right => onRight.Invoke(),
                CellDirection.Any   => onAny.Invoke(),
                _                   => throw new("Unknown Side!"),
            };
        }
    }
}
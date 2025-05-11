using System;

namespace FelineFellas
{
    [Serializable]
    [Flags]
    [JetBrains.Annotations.UsedImplicitly]
    public enum Direction
    {
        Unknown = 0,
        Up = 1,
        Right = 2,
        Down = 3,
        Left = 4,
    }

    public static class DirectionExtensions
    {
        // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
        public static Coordinates ToCoordinates(this Direction @this)
        {
            var coordinates = new Coordinates();

            if (@this.HasFlag(Direction.Up))
                coordinates = coordinates.Add(row: 1);

            if (@this.HasFlag(Direction.Right))
                coordinates = coordinates.Add(column: 1);

            if (@this.HasFlag(Direction.Down))
                coordinates = coordinates.Add(row: -1);

            if (@this.HasFlag(Direction.Left))
                coordinates = coordinates.Add(column: -1);

            return coordinates;
        }
    }
}
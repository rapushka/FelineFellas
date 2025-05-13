using System;
using UnityEngine;

namespace FelineFellas
{
    [Serializable]
    public struct Coordinates
    {
        [SerializeField] private int _row;
        [SerializeField] private int _column;

        public Coordinates(int row, int column)
        {
            _row = row;
            _column = column;
        }

        public int Row    => _row;
        public int Column => _column;

        public Coordinates Add(Coordinates other)
            => new(_row + other._row, _column + other._column);

        public Coordinates Add(int row = 0, int column = 0)
            => new(_row + row, _column + column);

        public Coordinates Multiply(int value)
            => new(_row * value, _column * value);

        public float DistanceTo(Coordinates other)
        {
            float num1 = _row - other._row;
            float num2 = _column - other._column;

            return (float)Math.Sqrt((double)num1 * num1 + (double)num2 * num2);
        }

        public override string ToString() => $"[{Row}; {Column}]";
    }
}
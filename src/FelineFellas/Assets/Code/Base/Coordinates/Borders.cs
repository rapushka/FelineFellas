namespace FelineFellas
{
    public readonly struct Borders
    {
        private readonly Coordinates _min;
        private readonly Coordinates _max;

        public Borders(Coordinates min, Coordinates max)
        {
            _min = min;
            _max = max;
        }

        public Coordinates Clamp(Coordinates source)
            => new(
                row: source.Row.Clamp(_min.Row, _max.Row),
                column: source.Column.Clamp(_min.Column, _max.Column)
            );

        public override string ToString() => $"(min: {_min}, max: {_max})";
    }
}
namespace FelineFellas
{
    public interface IIdentifiesService : IService
    {
        int Next();

        void Reset();
    }

    public class SimplestIdentifiesService : IIdentifiesService
    {
        private int _counter;

        public int Next() => _counter++;

        public void Reset()
        {
            if (_counter >= 1_000_000_000)
                _counter = 0;
        }
    }
}
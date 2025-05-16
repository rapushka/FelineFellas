using System.Collections.Generic;
using System.Linq;
using UnityRandom = UnityEngine.Random;

// ReSharper disable PossibleMultipleEnumeration  - go fuck yourself

namespace FelineFellas
{
    public interface IRandomService : IService
    {
        float Range(float min, float max);

        T PickRandom<T>(IEnumerable<T> collection);

        IWeighted PickRandom(IEnumerable<IWeighted> collection);
    }

    public class RandomService : IRandomService
    {
        public float Range(float min, float max) => UnityRandom.Range(min, max);

        public T PickRandom<T>(IEnumerable<T> collection)
        {
            var total = collection.Count();
            var index = UnityRandom.Range(0, total);

            return collection.ElementAtOrDefault(index)
                ?? throw new("Index out of bounds");
        }

        public IWeighted PickRandom(IEnumerable<IWeighted> collection)
        {
            var totalWeight = collection.Sum(x => x.Weight);
            var randomValue = UnityRandom.value * totalWeight;

            foreach (var item in collection)
            {
                randomValue -= item.Weight;
                if (randomValue <= 0)
                    return item;
            }

            return null;
        }
    }
}
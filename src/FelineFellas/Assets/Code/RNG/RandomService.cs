using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ReSharper disable PossibleMultipleEnumeration  - go fuck yourself

namespace FelineFellas
{
    public interface IRandomService : IService
    {
        T PickRandom<T>(IEnumerable<T> collection);

        IWeighted PickRandom(IEnumerable<IWeighted> collection);
    }

    public class RandomService : IRandomService
    {
        public T PickRandom<T>(IEnumerable<T> collection)
        {
            var total = collection.Count();
            var index = Random.Range(0, total);

            return collection.ElementAtOrDefault(index)
                ?? throw new("Index out of bounds");
        }

        public IWeighted PickRandom(IEnumerable<IWeighted> collection)
        {
            var totalWeight = collection.Sum(x => x.Weight);
            var randomValue = Random.value * totalWeight;

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
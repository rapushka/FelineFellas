using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityRandom = UnityEngine.Random;

// ReSharper disable PossibleMultipleEnumeration  - go fuck yourself

namespace FelineFellas
{
    public interface IRandomService : IService
    {
        float Range(float min, float max);

        T PickRandom<T>(ICollection<T> collection);
        T PickRandom<T>(IEnumerable<T> collection);
        T PickRandom<T>(IGroup<T> collection) where T : class, IEntity;

        IWeighted PickRandom(IEnumerable<IWeighted> collection);
    }

    public class RandomService : IRandomService
    {
        public float Range(float min, float max) => UnityRandom.Range(min, max);

        public T PickRandom<T>(ICollection<T> collection)
        {
            var total = collection.Count;

            if (total == 0)
                throw new("Collection is empty");

            var index = UnityRandom.Range(0, total);

            return collection.ElementAtOrDefault(index)
                ?? throw new($"Index {index} is out of bounds");
        }

        public T PickRandom<T>(IEnumerable<T> collection)
        {
            var total = collection.Count();

            if (total == 0)
                throw new("Collection is empty");

            var index = UnityRandom.Range(0, total);

            return collection.ElementAtOrDefault(index)
                ?? throw new($"Index {index} is out of bounds");
        }

        public T PickRandom<T>(IGroup<T> collection) where T : class, IEntity
        {
            var total = collection.count;
            var index = UnityRandom.Range(0, total);

            var counter = 0;
            foreach (var entity in collection)
            {
                if (counter == index)
                    return entity;

                counter++;
            }

            throw new("Index out of bounds");
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
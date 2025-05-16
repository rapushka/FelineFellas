using System.Linq;
using UnityEngine;

namespace FelineFellas
{
    public interface IRandomService : IService
    {
        IWeighted PickRandom(IWeighted[] collection);
    }

    public class RandomService : IRandomService
    {
        public IWeighted PickRandom(IWeighted[] collection)
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
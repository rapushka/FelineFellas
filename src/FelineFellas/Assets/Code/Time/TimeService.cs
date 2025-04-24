using UnityEngine;

namespace FelineFellas
{
    public interface ITimeService : IService
    {
        float AnimationDelta { get; }
    }

    public class TimeService : ITimeService
    {
        public float AnimationDelta => Time.deltaTime;
    }
}
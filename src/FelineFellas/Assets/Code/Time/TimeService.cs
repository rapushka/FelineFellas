using UnityEngine;

namespace FelineFellas
{
    public interface ITimeService : IService
    {
        float AnimationDelta { get; }
        float RealDelta      { get; }
    }

    public class TimeService : ITimeService
    {
        public float AnimationDelta => Time.deltaTime;

        public float RealDelta => Time.deltaTime;
    }
}
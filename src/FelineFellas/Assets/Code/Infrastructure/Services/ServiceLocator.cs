using System;
using UnityEngine;

namespace FelineFellas
{
    public static class ServiceLocator
    {
        public static void Register<TService>(TService service) where TService : IService
        {
            Validate<TService>();
            Service<TService>.Instance = service;
        }

        public static TService Resolve<TService>()
            where TService : IService
        {
            Validate<TService>();
            return Service<TService>.Instance;
        }

        private static void Validate<TService>()
            where TService : IService
        {
#if UNITY_EDITOR
            if (!typeof(TService).IsInterface)
                Debug.LogError("All Services must be Registered and Resolved by Interface!");
#endif
        }

        private static class Service<T>
            where T : IService
        {
            private static T _instance;

            public static T Instance
            {
                get => _instance ?? throw new InvalidOperationException($"the {typeof(T).Name} Service isn't initialized!");
                set => _instance = value ?? throw new NullReferenceException();
            }
        }
    }
}
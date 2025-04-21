using System;
using System.Collections.Generic;

namespace FelineFellas
{
    public class TypeDictionary<TBase> : Dictionary<Type, TBase>
    {
        public T Get<T>() where T : TBase => (T)this[typeof(T)];

        public void Add<T>(T newItem) where T : TBase => Add(newItem.GetType(), newItem);

        public T GetOrAdd<T>(Func<T> create)
            where T : TBase
        {
            var type = typeof(T);

            if (TryGetValue(type, out var item))
                return (T)item;

            var newItem = create.Invoke();
            Add(newItem);
            return newItem;
        }
    }
}
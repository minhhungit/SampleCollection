using System;
using System.Collections.Concurrent;

namespace Counter.Core
{
    public class CounterCreator
    {
        private static readonly Lazy<CounterCreator> Lazy =
               new Lazy<CounterCreator>(() => new CounterCreator());

        public static CounterCreator Instance => Lazy.Value;

        public static ConcurrentDictionary<string, Counter> Counters { get; }

        static CounterCreator()
        {
            Counters = new ConcurrentDictionary<string, Counter>();
        }

        public Counter Create(string counterKey)
        {
            var ins = (Counter)Activator.CreateInstance(typeof(Counter));
            return Counters.GetOrAdd(counterKey, ins);
        }
    }

    public class CounterCreator<T> where T : BaseCounter, new()
    {
        private static readonly Lazy<CounterCreator<T>> Lazy =
               new Lazy<CounterCreator<T>>(() => new CounterCreator<T>());

        public static CounterCreator<T> Instance => Lazy.Value;

        public static ConcurrentDictionary<string, T> Counters { get; }
        static T _counterIns = (T)Activator.CreateInstance(typeof(T));

        static CounterCreator()
        {
            Counters = new ConcurrentDictionary<string, T>();
        }

        public T Create(T obj = null)
        {
            if (obj == null)
            {
                return Counters.GetOrAdd(_counterIns.CounterKey, _counterIns);
            }
            else
            {
                return Counters.GetOrAdd(obj.CounterKey, obj);
            }            
        }
    }
}

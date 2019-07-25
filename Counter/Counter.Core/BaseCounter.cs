using System;
using System.Threading;

namespace Counter.Core
{
    public interface IBaseCounter
    {
        long CurrentValue { get; }
        string CounterKey { get; }
    }

    public abstract class BaseCounter : IBaseCounter
    {
        private long _currentValue = 0;

        public DateTime CounterDate { get; }

        public long IncrementBy(long value)
        {
            return Interlocked.Add(ref _currentValue, value);
        }

        public long DecrementBy(long value)
        {
            return Interlocked.Add(ref _currentValue, -value);
        }

        public long Exchange(long newValue)
        {
            return Interlocked.Exchange(ref _currentValue, newValue);
        }

        public long CurrentValue => Interlocked.Read(ref _currentValue);

        public abstract string CounterKey { get; }
    }

    public class Counter : BaseCounter
    {
        public override string CounterKey { get; }
    }
}

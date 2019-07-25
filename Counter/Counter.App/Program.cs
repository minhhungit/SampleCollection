using Counter.Core;
using System;
using System.Linq;

namespace Counter.App
{
    public enum ServiceType
    {
        FirstService,
        Se7enService,
    }

    public class SimpleCounter : BaseCounter
    {
        public Guid SupplierId { get; set; }
        public ServiceType ServiceType { get; set; }

        public override string CounterKey => $"{SupplierId:N}_{ServiceType}";
    }

    class Program
    {
        static Core.Counter counter01 = CounterCreator.Instance.Create("base_01");
        static Core.Counter counter02 = CounterCreator.Instance.Create("base_02");

        static SimpleCounter customCounter = CounterCreator<SimpleCounter>.Instance.Create();
        static SimpleCounter customCounterShadow = CounterCreator<SimpleCounter>.Instance.Create(); // = customCounter

        static SimpleCounter customCounterConstruct = CounterCreator<SimpleCounter>.Instance.Create(new SimpleCounter { SupplierId = new Guid("df1bc6fe-0a73-4865-b2de-89e111e9defb"), ServiceType = ServiceType.FirstService }); // = customCounter

        static void Main()
        {
            // value = 2
            counter01.IncrementBy(2);

            // value = 8
            counter02.IncrementBy(5);
            counter02.IncrementBy(3);

            // value = 25
            customCounter.IncrementBy(6);
            customCounter.IncrementBy(9);
            customCounterShadow.IncrementBy(10);

            // value  = 12
            customCounterConstruct.IncrementBy(6);
            customCounterConstruct.DecrementBy(3);
            customCounterConstruct.Exchange(5);
            customCounterConstruct.IncrementBy(7);

            foreach (var c in CounterCreator.Counters)
            {
                Console.WriteLine($"{c.Key}: {c.Value.CurrentValue}");
            }

            foreach (var c in CounterCreator<SimpleCounter>.Counters)
            {
                Console.WriteLine($"{c.Key}: {c.Value.CurrentValue}");
            }

            // ---------------------------------------------
            for (int i = 0; i < 100; i++)
            {
                var tmpCounter = CounterCreator<SimpleCounter>.Instance.Create(new SimpleCounter { SupplierId = Guid.NewGuid(), ServiceType = i % 7 == 0 ? ServiceType.Se7enService : ServiceType.FirstService });
                tmpCounter.IncrementBy(i);
            }

            var se7enCounters = CounterCreator<SimpleCounter>.Counters.Where(x => x.Value.ServiceType == ServiceType.Se7enService).OrderBy(x => x.Value.CurrentValue);
            foreach (var c in se7enCounters)
            {
                Console.WriteLine($"  - se7en {c.Key}: {c.Value.CurrentValue}");
            }

            Console.WriteLine("Hello World!");
        }
    }
}

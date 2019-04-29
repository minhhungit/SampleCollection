using System;

namespace ConfigPattern_Action
{
    class Program
    {
        static void Main(string[] args)
        {
            MyHelper.Initialize("Hello", new TimeSpan(1, 0, 0));
            Console.WriteLine(MyHelper.DoSomething());

            Console.WriteLine("  ----");

            MyHelper.Initialize(x =>
            {
                x.WithConnection("Hello{it.minhhung@gmail.com}");
                x.WithTimeout(new TimeSpan(0, 2, 0)); // 120 seconds
            });
            Console.WriteLine(MyHelper.DoSomething());

            Console.ReadKey();
        }
    }

    public class MyHelper
    {
        public class Configuration
        {
            public string ConnectionString { get; private set; }
            public TimeSpan Timeout { get; private set; }

            public void WithConnection(string v)
            {
                ConnectionString = v;
            }

            public void WithTimeout(TimeSpan v)
            {
                Timeout = v;
            }
        }

        private static readonly Configuration _config = new Configuration();

        public static void Initialize(string connectionString, TimeSpan timeout)
        {
            _config.WithConnection(connectionString);
            _config.WithTimeout(timeout);
        }

        public static void Initialize(Action<Configuration> action)
        {
            action(_config);
        }

        public static string DoSomething()
        {
            return $"- ConnectionString: {_config.ConnectionString} \n- Timeout: {_config.Timeout.TotalSeconds} seconds";
        }
    }
}
